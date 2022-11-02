namespace CUGOJ.Backend.Tools;
public static class ParamParser
{
    private static Dictionary<string, string> argKeys = new Dictionary<string, string>()
    {
        {"localhost" ,"-localhost"},
        {"local","-localhost"},
        {"lo","-localhost"},
        {"trace","trace"},
        {"t","trace"},
        {"log","log"},
        {"l","log"},
        {"siloport","siloport"},
        {"s","siloport"},
        {"gatewayport","gatewayport"},
        {"g","gatewayport"},
        {"env","env"},
        {"e","env"},
        {"db","db"},
        {"redis","redis"},
        {"rabbit","rabbit"},
        {"neo","neo"},
        {"updateDB","-updateDB"},
        {"debug","-debug" },
        {"admin","admin" },
        {"nossl","-nossl" }
    };

    public static Dictionary<string, string> ParseArgs(string[] args,bool AllowReplicate = false)
    {
        Dictionary<string, string> res = new();
        string? key = null;
        foreach (var arg in args)
        {
            if (arg == "-")
            {
                throw new Exception("未知的参数键-");
            }
            if (arg.StartsWith('-'))
            {
                if (key != null)
                {
                    throw new Exception("缺失参数值:-" + key);
                }
                else
                {
                    if (!argKeys.TryGetValue(arg.Substring(1), out key))
                    {
                        throw new Exception("未知的参数键" + arg);
                    }
                    if (key.StartsWith('-'))
                    {
                        key = key.Substring(1);
                        if (!AllowReplicate && res.ContainsKey(key)) 
                        {
                            throw new Exception("重复设置参数" + key);
                        }
                        res[key] = "true";
                        key = null;
                    }
                }
            }
            else
            {
                if (key == null)
                {
                    throw new Exception("未知参数键" + arg + "请使用-来标识参数键");
                }
                if (res.ContainsKey(key))
                {
                    throw new Exception("重复设置参数" + key);
                }
                res[key] = arg;
                key = null;
            }
        }
        return res;
    }
}