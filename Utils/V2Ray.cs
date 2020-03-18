using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace V2RayShell
{
	public static class V2Ray
	{
		#region Props

		public const string V2RAY_CORE = "v2ray.exe";
		public const string V2RAY_CORE_WITHOUTEXT = "v2ray";
		public const string V2RAY_CTL = "v2ctl.exe";


		private static readonly Regex _regexVersion = new Regex(@"v2ray\s+(\d+).(\d+).(\d+)", RegexOptions.IgnoreCase);

		public static bool CoreExsis => File.Exists(V2RAY_CORE) && File.Exists(V2RAY_CTL);

		public static Version Version
		{
			get
			{
				if (!CoreExsis) return null;
				var run = Utils.StartProcess(V2RAY_CORE, "-version");
				if (string.IsNullOrEmpty(run)) return null;
				var match = _regexVersion.Match(run);
				return match.Success ? new Version(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value)) : null;
			}
		}
		#endregion

		#region Methods


		#endregion

		#region Template

		public static readonly string AccessLogPath = Utils.GetTempPath("V2RayShell_Core_Access.log");

		public static readonly string ErrorLogPath = Utils.GetTempPath("V2RayShell_Core_Error.log");

		public static string GenerateVNextConf(ServerObject svc, int listenPort, string listenAddr = "127.0.0.1")
		{
			var sso = new StreamSettingsObject { network = svc.net, security = svc.tls.Replace("none","") };
			switch (svc.net)
			{
				case "tcp":
					sso.tcpSettings = new TcpObject { type = svc.type ?? "none" };
					break;
				case "ws":
					sso.wsSettings = new WebSocketObject { path = svc.path, headers = new Dictionary<string, string> { { "Host", svc.host } } };
					break;
				case "http":
					sso.httpSettings = new HttpObject { path = svc.path, host = svc.host.Split(',') };
					break;
				default:
					break;
			}

			var outbound = new OutboundObject
			{
				protocol = "vmess",
				mux = new MuxObject{enabled = false,concurrency = 8},
				tag = "outBound_PROXY",
				streamSettings = sso,
				settings = new OutboundConfigurationObject
				{
					vnext = new []{new Vnext
					{
						address = svc.add,
						port = svc.port,
						users = new []{new User
						{
							alterId = svc.aid,
							id = svc.id,
							level = 0,
							security = "auto"
						} }
					}}
				}

			}.SerializeToJson();
			var inbound = $@"{{""port"":{listenPort},""listen"":""{listenAddr}"",""protocol"":""socks"",""settings"":{{""auth"":""noauth"",""udp"":true,""ip"":""{listenAddr}"",""clients"":null}},""streamSettings"":null}}";
			return $@"{{""log"":{{""access"":""{AccessLogPath.Replace("\\", @"\\")}"",""error"":""{ErrorLogPath.Replace("\\", @"\\")}"",""loglevel"":""warning""}},""inbounds"":[{inbound}],""outbounds"":[{outbound}],""routing"":{{""domainStrategy"":""IPOnDemand"",""rules"":[{{""type"":""field"",""ip"":[""0.0.0.0/8"",""10.0.0.0/8"",""100.64.0.0/10"",""127.0.0.0/8"",""169.254.0.0/16"",""172.16.0.0/12"",""192.0.0.0/24"",""192.0.2.0/24"",""192.168.0.0/16"",""198.18.0.0/15"",""198.51.100.0/24"",""203.0.113.0/24"",""::1/128"",""fc00::/7"",""fe80::/10""],""outboundTag"":""direct""}}]}},""dns"":{{""hosts"":{{}},""servers"":[""223.5.5.5"",""114.114.114.114"",""localhost""]}},""policy"":{{""system"":{{""statsInboundUplink"":false,""statsInboundDownlink"":false}}}},""other"":{{}}}}";
		}

		#endregion
	}

	#region Config

	public class ServerObject
	{
		public ServerObject()
		{
		}

		public string ToURL()
		{
			//TODO need encode ps ????
			return $"vmess://{Convert.ToBase64String(Encoding.UTF8.GetBytes(this.SerializeToJson()))}";
		}

		public static List<ServerObject> GetServers(string vmessURL)
		{
			var serverUrls = vmessURL.Contains("vmess://") ?
				vmessURL.Split('\r', '\n') :
				Encoding.UTF8.GetString(Convert.FromBase64String(vmessURL)).Split('\r', '\n');

			var servers = new List<ServerObject>();
			foreach (var serverUrl in serverUrls)
			{
				if(TryParse(serverUrl, out var saba))
					servers.Add(saba);
			}
			return servers;
		}

		public static bool TryParse(string input, out ServerObject svc)
		{
			if (input.StartsWith("vmess://", StringComparison.OrdinalIgnoreCase))
				input = input.Remove(0, 8);
			try
			{
				var de64 = Encoding.UTF8.GetString(Convert.FromBase64String(input));
				svc = Utils.DeSerializeJsonObject<ServerObject>(de64);
				return true;
			}
			catch (Exception e)
			{
				Logging.LogUsefulException(e);
				svc = null;
				return false;
			}
		}

		/// <summary>
		/// 版本
		/// </summary>
		public string v { get; set; }
		/// <summary>
		/// 备注
		/// </summary>
		public string ps { get; set; }
		/// <summary>
		/// 远程服务器地址
		/// </summary>
		public string add { get; set; }
		/// <summary>
		/// 远程服务器端口
		/// </summary>
		public int port { get; set; }
		/// <summary>
		/// 远程服务器ID
		/// </summary>
		public string id { get; set; }
		/// <summary>
		/// 远程服务器额外ID
		/// </summary>
		public int aid { get; set; }
		/// <summary>
		/// 传输协议tcp,kcp,ws
		/// </summary>
		public string net { get; set; }
		/// <summary>
		/// 伪装类型
		/// </summary>
		public string type { get; set; }
		/// <summary>
		/// 伪装的域名
		/// </summary>
		public string host { get; set; }
		/// <summary>
		/// path
		/// </summary>
		public string path { get; set; }
		/// <summary>
		/// 底层传输安全
		/// </summary>
		public string tls { get; set; }

		/// <summary>
		/// 分组，默认空
		/// </summary>
		public string group { get; set; } = "";
	}


	public class OutboundObject
	{
		// /// <summary>
		// /// 用于发送数据的 IP 地址，当主机有多个 IP 地址时有效，默认值为"0.0.0.0"。
		// /// </summary>
		// public string sendThrough { get; set; }
		public string protocol { get; set; }
		public OutboundConfigurationObject settings { get; set; }
		public string tag { get; set; }
		public StreamSettingsObject streamSettings { get; set; }

		//public Proxysettings proxySettings { get; set; }
		public MuxObject mux { get; set; }
	}


	public class OutboundConfigurationObject
	{
		public Vnext[] vnext { get; set; }
	}

	public class Vnext
	{
		public string address { get; set; }
		public int port { get; set; }
		public User[] users { get; set; }
	}

	public class User
	{
		public string id { get; set; }
		public int alterId { get; set; }
		public string security { get; set; }
		public int level { get; set; }
	}


	public class MuxObject
	{
		/// <summary>
		/// 是否启用 Mux 转发请求
		/// </summary>
		public bool enabled { get; set; }
		/// <summary>
		/// 最大并发连接数。最小值1，最大值1024，缺省默认值8。
		/// </summary>
		public int concurrency { get; set; }
	}



	public class StreamSettingsObject
	{
		/// <summary>
		/// "tcp" | "kcp" | "ws" | "http" | "domainsocket" | "quic"
		/// </summary>
		public string network { get; set; }
		/// <summary>
		/// "none" | "tls"
		/// </summary>
		public string security { get; set; }
		public TLSObject tlsSettings { get; set; }
		public TcpObject tcpSettings { get; set; }
		public KcpObject kcpSettings { get; set; }
		public WebSocketObject wsSettings { get; set; }
		public HttpObject httpSettings { get; set; }
	}


	public class HttpObject
	{
		public string[] host { get; set; }
		public string path { get; set; }
	}


	public class WebSocketObject
	{
		public string path { get; set; }
		public Dictionary<string, string> headers { get; set; }
	}


	public class KcpObject
	{
		public int mtu { get; set; }
		public int tti { get; set; }
		public int uplinkCapacity { get; set; }
		public int downlinkCapacity { get; set; }
		public bool congestion { get; set; }
		public int readBufferSize { get; set; }
		public int writeBufferSize { get; set; }
		public HeaderObject header { get; set; }
	}

	public class HeaderObject
	{
		public string type { get; set; }
	}


	public class TLSObject
	{
		public string serverName { get; set; }
		public bool allowInsecure { get; set; }
		public string[] alpn { get; set; }
		public CertificateObject[] certificates { get; set; }
		public bool disableSystemRoot { get; set; }
	}


	public class CertificateObject
	{
		public string usage { get; set; }
		public string certificateFile { get; set; }
		public string keyFile { get; set; }
		public string[] certificate { get; set; }
		public string[] key { get; set; }
	}


	public class TcpObject
	{
		public string type { get; set; }
		public HTTPRequestObject request { get; set; }
		public HTTPResponseObject response { get; set; }
	}


	public class HTTPRequestObject
	{
		public string version { get; set; }
		public string method { get; set; }
		public string[] path { get; set; }
		public HTTPRequestHeaders headers { get; set; }
	}

	public class HTTPRequestHeaders
	{
		public string[] Host { get; set; }
		public string[] UserAgent { get; set; }
		public string[] AcceptEncoding { get; set; }
		public string[] Connection { get; set; }
		public string Pragma { get; set; }
	}



	public class HTTPResponseObject
	{
		public string version { get; set; }
		public string status { get; set; }
		public string reason { get; set; }
		public HTTPResponseHeaders headers { get; set; }
	}

	public class HTTPResponseHeaders
	{
		public string[] ContentType { get; set; }
		public string[] TransferEncoding { get; set; }
		public string[] Connection { get; set; }
		public string Pragma { get; set; }
	}





	/*

	public class V2RayConfig
	{
		/// <summary>
		/// 日志配置，表示 V2Ray 如何输出日志。
		/// </summary>
		public V2RayLog log { get; set; }

		// /// <summary>
		// /// 内置的远程控置 API，详见远程控制配置。
		// /// </summary>
		// public V2RayApi api { get; set; }

		/// <summary>
		/// 内置的 DNS 服务器，若此项不存在，则默认使用本机的 DNS 设置。详见DNS 配置
		/// </summary>
		public V2RayDns dns { get; set; }

		// /// <summary>
		// /// 当此项存在时，开启统计信息。
		// /// </summary>
		// public object stats { get; set; }
		public V2RayRouting routing { get; set; }

		/// <summary>
		/// 本地策略可进行一些权限相关的配置，详见本地策略
		/// </summary>
		public V2RayPolicy policy { get; set; }

		// /// <summary>
		// /// 反向代理配置。
		// /// </summary>
		//public V2RayReverse reverse { get; set; }

		/// <summary>
		/// 一个数组，每个元素是一个入站连接配置。
		/// </summary>
		public V2RayInbound[] inbounds { get; set; }

		/// <summary>
		/// 一个数组，每个元素是一个出站连接配置。列表中的第一个元素作为主出站协议。当路由匹配不存在或没有匹配成功时，流量由主出站协议发出。
		/// </summary>
		public object[] outbounds { get; set; }

		// /// <summary>
		// /// 用于配置 V2Ray 如何与其它服务器建立和使用网络连接。详见底层传输配置
		// /// </summary>
		// public Transport transport { get; set; }
	}


	public class V2RayInbound
	{
		public int port { get; set; }
		public string listen { get; set; }
		public string tag { get; set; }
		public string protocol { get; set; }
		public Settings settings { get; set; }
		public Sniffing sniffing { get; set; }
	}

	public class Settings
	{
		public string auth { get; set; }
		public bool udp { get; set; }
		public string ip { get; set; }
	}

	public class Sniffing
	{
		public bool enabled { get; set; }
		public string[] destOverride { get; set; }
	}


	public class V2RayLog
	{
		public string access { get; set; }
		public string error { get; set; }
		public string loglevel { get; set; }
	}



	// public class V2RayApi
	// {
	//     public string tag { get; set; }
	//     /// <summary>
	//     /// HandlerService 一些对于入站出站代理进行修改的 API，可用的功能如下：
	//     ///     添加一个新的入站代理；
	//     ///     添加一个新的出站代理；
	//     ///     删除一个现有的入站代理；
	//     ///     删除一个现有的出站代理；
	//     ///     在一个入站代理中添加一个用户（仅支持 VMess）；
	//     ///     在一个入站代理中删除一个用户（仅支持 VMess）；
	//     /// LoggerService 支持对内置 Logger 的重启，可配合 logrotate 进行一些对日志文件的操作。
	//     /// StatsService 内置的数据统计服务，详见<a href="https://www.v2ray.com/chapter_02/stats.html">统计信息</a>。
	//     /// </summary>
	//     public string[] services { get; set; }
	// }



	public class V2RayDns
	{
		//public V2rayDnsHosts hosts { get; set; }
		public string[] servers { get; set; }//TODO
		public string clientIp { get; set; }
		public string tag { get; set; }
	}



	public class V2RayRouting
	{
		public string domainStrategy { get; set; }
		public V2RayRule[] rules { get; set; }
	}

	public class V2RayRule
	{
		public string type { get; set; }
		public string[] ip { get; set; }
		public string outboundTag { get; set; }
		public string[] domain { get; set; }
	}


	public class V2RayPolicy
	{
		public Dictionary<string, V2RayLevelPolicy> levels { get; set; }
		public V2RayPolicySystem system { get; set; }
	}


	public class V2RayLevelPolicy
	{
		/// <summary>
		/// 连接建立时的握手时间限制。单位为秒。默认值为4。在入站代理处理一个新连接时，在握手阶段（比如 VMess 读取头部数据，判断目标服务器地址），如果使用的时间超过这个时间，则中断该连接。
		/// </summary>
		public int handshake { get; set; }
		/// <summary>
		/// 连接空闲的时间限制。单位为秒。默认值为300。在入站出站代理处理一个连接时，如果在 connIdle 时间内，没有任何数据被传输（包括上行和下行数据），则中断该连接。
		/// </summary>
		public int connIdle { get; set; }
		/// <summary>
		/// 当连接下行线路关闭后的时间限制。单位为秒。默认值为2。当服务器（如远端网站）关闭下行连接时，出站代理会在等待 uplinkOnly 时间后中断连接。
		/// </summary>
		public int uplinkOnly { get; set; }
		/// <summary>
		/// 当连接上行线路关闭后的时间限制。单位为秒。默认值为5。当客户端（如浏览器）关闭上行连接时，入站代理会在等待 downlinkOnly 时间后中断连接。
		/// </summary>
		public int downlinkOnly { get; set; }
		/// <summary>
		/// 当值为true时，开启当前等级的所有用户的上行流量统计。
		/// </summary>
		public bool statsUserUplink { get; set; }
		/// <summary>
		/// 当值为true时，开启当前等级的所有用户的下行流量统计。
		/// </summary>
		public bool statsUserDownlink { get; set; }
		/// <summary>
		/// 每个连接的内部缓存大小。单位为 kB。当值为0时，内部缓存被禁用。
		/// </summary>
		public int bufferSize { get; set; }
	}

	public class V2RayPolicySystem
	{
		/// <summary>
		/// 当值为true时，开启所有入站代理的上行流量统计。
		/// </summary>
		public bool statsInboundUplink { get; set; }
		/// <summary>
		/// 当值为true时，开启所有入站代理的下行流量统计。
		/// </summary>
		public bool statsInboundDownlink { get; set; }
	}


	// public class V2RayReverse
	// {
	//     public V2RayBridge[] bridges { get; set; }
	//     public V2RayBridge[] portals { get; set; }
	// }
	//
	// public class V2RayBridge
	// {
	//     public string tag { get; set; }
	//     public string domain { get; set; }
	// }

	*/
	#endregion
}
