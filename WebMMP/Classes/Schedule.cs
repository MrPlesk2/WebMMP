using System.Text.Json;
using WebMMP.Scripts;

public class Schedule
{
    public List<Group> Groups { get; private set; }
    private static List<int> groupsId;
    private static HttpClient client = new HttpClient();

    public Schedule()
    {
        Groups = new List<Group>();
    }

    async public Task Setup()
    {
        await UpdateGroupsId();
        await UpdateGroups();
        UpdateBook();
    }

    async public Task UpdateGroupsId()
    {
        groupsId = new List<int>();
        for (var course = 1; course <= 6; course++)
        {
            var response = await client.GetStreamAsync($"https://urfu.ru/api/v2/schedule/divisions/62404/groups?course={course}");
            var ids = await JsonSerializer.DeserializeAsync<GroupId[]>(response);
            foreach (var id in ids)
                groupsId.Add(id.id);
        }
    }

    async public Task UpdateGroups()
    {
        Groups = new List<Group>();
        GC.Collect();
        var date = DateTime.Now.ToString("yyyy-MM-dd");
        foreach (var id in groupsId)
        {
            //$"https://urfu.ru/api/v2/schedule/groups/{id}/schedule?date_gte={date}&date_lte={date}"
            var response = await client.GetStreamAsync($"https://urfu.ru/api/v2/schedule/groups/{id}/schedule?date_gte={date}&date_lte={date}");
            var group = await JsonSerializer.DeserializeAsync<Group>(response);
            Groups.Add(group);
        }
    }

    public void UpdateBook()
    {
        AuditoryBook.ClearBook();
        lock (this)
        {
            AuditoryBook.BookIp = new Dictionary<string, string>();
            foreach (var pair in GetAllPairs(TimeOperations.GetPairNumber(DateTime.UtcNow.AddHours(5))))
            {
                AuditoryBook.Book[pair.AuditoryTitle] = BookTypes.lesson;
                AuditoryBook.BookIp[pair.AuditoryTitle] = "admin";
            }
        }
    }

    private Lesson?[] GetAllPairs(int pairNumber)
        => Groups.Select(group => group.GetPair(pairNumber))
                 .Where(pair => pair is not null)
                 .Where(pair => pair.AuditoryTitle is not null)
                 .ToArray();

    private record GroupId(int id);
}