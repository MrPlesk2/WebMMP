using System.Text.Json.Serialization;

public class Group
{
    [JsonPropertyName("events")]
    public List<Lesson> Lessons { get; set; }

    public Lesson? GetPair(int pairNumber) 
        => Lessons.Where(lesson => lesson.PairNumber == pairNumber).FirstOrDefault();
}