namespace patching_api;

public class System
{
    private string system_id;
    public double uptime{ get; private set;}
    public string region{ get; private set;}
    private DateTime last_patch;
    private int days_since;
    
    public System(string system_id, double uptime, string region, DateTime last_patch)
    {
        this.system_id = system_id;
        this.uptime = uptime;
        this.region = region;
        this.last_patch = last_patch;
    }

    public void setDaysSince()
    {
        this.days_since = (DateTime.Now - last_patch).Days;
    }
    
}