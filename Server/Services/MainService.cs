using Grpc.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Tema2.Models;
using Tema2.Protos.Main;

namespace Tema2.Services
{
    public class MainService : Main.MainBase
    {
        public override Task<ResponseZodiac> GetZodiac(RequestZodiac request, ServerCallContext context)
        {
            ResponseZodiac responseZodiac;
            DateTime birthday = DateTime.Parse(request.UserData.Birthday);
            if (birthday.Month >= 12 || birthday.Month <= 2)
            {
                WinterService winterService = new WinterService();
                Tema2.Protos.Winter.RequestZodiac winterRequest = new Tema2.Protos.Winter.RequestZodiac() { UserData = new Tema2.Protos.Winter.UserData() { Birthday=request.UserData.Birthday } };
                responseZodiac = new ResponseZodiac() { Zodiac = new Zodiac() { ZodiacName = winterService.GetZodiac(winterRequest, context).Result.Zodiac.ZodiacName } };
            }
            else if (birthday.Month >= 3 && birthday.Month <= 5)
            {
                SpringService springService = new SpringService();
                Tema2.Protos.Spring.RequestZodiac springRequest = new Tema2.Protos.Spring.RequestZodiac() { UserData = new Tema2.Protos.Spring.UserData() { Birthday = request.UserData.Birthday } };
                responseZodiac = new ResponseZodiac() { Zodiac = new Zodiac() { ZodiacName = springService.GetZodiac(springRequest, context).Result.Zodiac.ZodiacName } };
            }
            else if (birthday.Month >= 6 && birthday.Month <= 8)
            {
                SummerService summerService = new SummerService();
                Tema2.Protos.Summer.RequestZodiac summerRequest = new Tema2.Protos.Summer.RequestZodiac() { UserData = new Tema2.Protos.Summer.UserData() { Birthday = request.UserData.Birthday } };
                responseZodiac = new ResponseZodiac() { Zodiac = new Zodiac() { ZodiacName = summerService.GetZodiac(summerRequest, context).Result.Zodiac.ZodiacName } };
            }
            else
            {
                AutumnService autumnService = new AutumnService();
                Tema2.Protos.Autumn.RequestZodiac autumnRequest = new Tema2.Protos.Autumn.RequestZodiac() { UserData = new Tema2.Protos.Autumn.UserData() { Birthday = request.UserData.Birthday } };
                responseZodiac = new ResponseZodiac() { Zodiac = new Zodiac() { ZodiacName = autumnService.GetZodiac(autumnRequest, context).Result.Zodiac.ZodiacName } };
            }

            return Task.FromResult<ResponseZodiac>(responseZodiac);
        }
    }
}
