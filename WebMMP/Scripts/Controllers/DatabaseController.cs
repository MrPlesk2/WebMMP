using Npgsql;
using System.Text.Json;

namespace WebMMP
{
    public class AuditoryInformation
    { 
        public string name { get; set; }
        public int book { get; set; }
    }
    public static class DatabaseController
    {


        public static string GetAllAuditoryBook()
        {
            var auditories = new List<AuditoryInformation>();

            foreach (var name in AuditoryBook.Book.Keys)
            { 
                var temp = new AuditoryInformation();
                temp.name = name;
                temp.book = (int)AuditoryBook.Book[name];
                auditories.Add(temp);
            }

            return JsonSerializer.Serialize(auditories);
        }

public static string GetCurrentAuditoryBook(string number) 
        {
            /*
            var time = DateTime.UtcNow.AddHours(5);
            var info = new AuditoryInformation();
            using (var nc = new Database(connString))
            {
                NpgsqlCommand npgc = new NpgsqlCommand($"select book from lesson inner join auditory on lesson.auditory_id = auditory.id where start_time='{Extra.ConvertTime(time)}' and auditory.number='{number}'", nc.table);

                NpgsqlDataReader ndr = npgc.ExecuteReader();

                while (ndr.Read())
                {
                    info.book = ndr.GetBoolean(0);
                }
            }
            return JsonSerializer.Serialize(info);
            */

            var info = new AuditoryInformation();
            info.book = (int)AuditoryBook.Book[number];

            return JsonSerializer.Serialize(info);
        }

        public static int SetCurrentAuditoryBook(string number, bool book, string ip)
        {
            /*
            var time = DateTime.UtcNow.AddHours(5);
            var stringBook = book ? "t" : "f";
            var auditory_id = 0;
            using (var nc = new Database(connString))
            {
                var npgc = new NpgsqlCommand($"select id from auditory where number = {number}", nc.table);

                NpgsqlDataReader ndr = npgc.ExecuteReader();

                while (ndr.Read())
                {
                    auditory_id = ndr.GetInt16(0);
                }
            }

            if (auditory_id != 0)
                using (var nc = new Database(connString))
                {
                    NpgsqlCommand npgc = new NpgsqlCommand($"update lesson set book='{stringBook}' where start_time='{Extra.ConvertTime(time)}' and auditory_id={auditory_id}", nc.table);

                    NpgsqlDataReader ndr = npgc.ExecuteReader();
                }
            */
            switch (AuditoryBook.Book[number])
            {
                case BookTypes.free:
                    if (!AuditoryBook.BookIp.ContainsValue(ip))
                    {
                        AuditoryBook.Book[number] = BookTypes.occupied;
                        AuditoryBook.BookIp[number] = ip;
                    }
                    else
                    {
                        return 1;
                    }
                    break;
                case BookTypes.lesson: 
                    return 1;
                case BookTypes.occupied:
                    if (ip == AuditoryBook.BookIp[number])
                    {
                        AuditoryBook.Book[number] = BookTypes.free;
                        AuditoryBook.BookIp.Remove(number);
                    }
                    else
                    {
                        return 1;
                    }
                    break;
            }
            return 0;
        }
    }
}
