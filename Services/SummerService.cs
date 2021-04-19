using Grpc.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Tema2.Models;
using Tema2.Protos.Summer;

namespace Tema2.Services
{
    public class SummerService : SummerZodiac.SummerZodiacBase
    {
        List<ZodiacData> ZodiacList { get; set; }
        public SummerService()
        {
            string jsonString = File.ReadAllText("WinterZodiac.json");
            ZodiacList = JsonSerializer.Deserialize<List<ZodiacData>>(jsonString);
        }
        public override Task<ResponseZodiac> GetZodiac(RequestZodiac request, ServerCallContext context)
        {
            DateTime birthday;
            ResponseZodiac responseZodiac;
            try
            {
                DateTime.TryParse(request.UserData.Birthday, out birthday);
            }
            catch
            {
                responseZodiac = new ResponseZodiac() { Zodiac = new Zodiac() { ZodiacName = "Error parsing birthday"} };
                return Task.FromResult<ResponseZodiac>(responseZodiac);
            }
            foreach(ZodiacData zodiac in ZodiacList)
            {
                DateTime zodiacStartDate = new DateTime(birthday.Year,zodiac.StartMonth,zodiac.StartDay);
                DateTime zodiacEndDate = new DateTime(birthday.Year, zodiac.StartMonth, zodiac.EndDay);
                zodiacEndDate.AddMonths(1);
                if (birthday >= zodiacStartDate && birthday <= zodiacEndDate)
                {
                    responseZodiac = new ResponseZodiac() { Zodiac = new Zodiac() { ZodiacName = zodiac.Name } };
                    return Task.FromResult<ResponseZodiac>(responseZodiac);
                }
            }
            responseZodiac = new ResponseZodiac() { Zodiac = new Zodiac() { ZodiacName = "Could not find zodiac" } };
            return Task.FromResult<ResponseZodiac>(responseZodiac);
        }
    }
}
