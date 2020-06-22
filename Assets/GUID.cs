[System.Serializable]
public class Guid
{
    public string id;
    public Guid()
    {
        id = System.Guid.NewGuid().ToString();
    }
}