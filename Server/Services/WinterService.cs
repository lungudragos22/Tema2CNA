﻿using Grpc.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Tema2.Models;
using Tema2.Protos.Winter;

namespace Tema2.Services
{
    public class WinterService : WinterZodiac.WinterZodiacBase
    {
        List<ZodiacData> ZodiacList { get; set; }
        public WinterService()
        {
            string jsonString = File.ReadAllText("WinterZodiac.json");
            ZodiacList = JsonSerializer.Deserialize<List<ZodiacData>>(jsonString);
        }
        public override Task<ResponseZodiac> GetZodiac(RequestZodiac request, ServerCallContext context)
        {
            DateTime birthday = DateTime.Parse(request.UserData.Birthday);
            ResponseZodiac responseZodiac;
            foreach(ZodiacData zodiac in ZodiacList)
            {
                DateTime zodiacStartDate = new DateTime(birthday.Year,zodiac.StartMonth,zodiac.StartDay);
                DateTime zodiacEndDate = new DateTime(birthday.Year, zodiac.StartMonth, zodiac.EndDay);
                zodiacEndDate = zodiacEndDate.AddMonths(1);
                Console.WriteLine(zodiacStartDate);
                Console.WriteLine(birthday);
                Console.WriteLine(zodiacEndDate);
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
