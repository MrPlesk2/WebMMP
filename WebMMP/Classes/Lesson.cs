using System.Text.Json.Serialization;

public record Lesson
{
    private static DateTime[] lessonStartTime = new[]
    {
        new DateTime(0,0,0, 9,0,0),
        new DateTime(0,0,0, 10,40,0),
        new DateTime(0,0,0, 12,50,0),
        new DateTime(0,0,0, 14,30,0),
        new DateTime(0,0,0, 16,10,0),
        new DateTime(0,0,0, 17,50,0),
        new DateTime(0,0,0, 19,30,0),
    };

    [JsonPropertyName("pairNumber")]
    public int PairNumber { get; set; }
    
    [JsonPropertyName("auditoryTitle")]
    public string AuditoryTitle { get; set; }
    [JsonPropertyName("auditoryLocation")]
    public string AuditoryLocation { get; set; }

    public DateTime GetStartTime()
        =>lessonStartTime[PairNumber-1];

    public bool IsOnTurgeneva()
        => AuditoryLocation == "Тургенева, 4";
}