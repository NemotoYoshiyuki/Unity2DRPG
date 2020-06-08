
public class Buff
{
    public StatusType statusType;
    public int count;
    public int value;

    public Buff(StatusType statusType, int count, int value)
    {
        this.statusType = statusType;
        this.count = count;
        this.value = value;
    }
}