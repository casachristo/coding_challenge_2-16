namespace patching_api;

public class System
{
    public string system_id { get; private set; }
    public double uptime{ get; private set;}
    public string region{ get; private set;}
    public string last_patch { get; private set; }
    private int days_since;
    
    public System(string system_id, double uptime, string region, string last_patch)
    {
        this.system_id = system_id;
        this.uptime = uptime;
        this.region = region;
        this.last_patch = last_patch;
    }

    public void setDaysSince()
    {
        var date = DateTime.Parse(this.last_patch);
        this.days_since = (DateTime.Now - date).Days;
    }
    
}