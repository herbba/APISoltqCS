using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using System;
using System.IO;
using System.Text;

namespace APISoltq.Controllers
{   
    
    [ApiController]
    [Route("[controller]")]
    public class TiedotController : ControllerBase
    {

       
        
        [HttpGet(Name = "GetTiedot")]
        //Tällä hetkellä ottaa vain hakaniemen tiedot mutta voidaan helposti laajentaa
        //voitaisiin laittaa ottamaan aikaväli ja halutaanko vaikka viikottaista dataa yms.

        public string Get(int id)
        {
            
            Sahko[] tiedot = TiedotHae(id);
            //antaa virheen jos rajapinnassa oli jotain häikkää
            if (tiedot.Length == 0) { Data[] dataerr = new Data[0];
                return JsonSerializer.Serialize<Data[]>(dataerr);
            }
            List <Data> datat = new List<Data>();
            for(int i = 0; i < tiedot.Length; i++)
            {   //vaihtaa Sahko luokasta Data luokkaan
                Data data = new Data();
                data.timestamp = tiedot[i].timestamp;
                data.value = tiedot[i].value;
                data.unit = tiedot[i].unit;
                datat.Add(data);
                
            }
            Data[] datas = datat.ToArray();
            Toimi.Kirjoita(tiedot);
            return JsonSerializer.Serialize<Data[]>(datas);
        }
        public static Sahko[] TiedotHae(int id)
        {
            //Tässä kohtaa id päättäisi mistä osoitteesta lähdettäisiin etsimään tietoa mutta tässä kohtaa olen vain hardkoodannut tämän etsimään oikean datan
            //mutta oikeasti laittaisin todennäköisesti taulukon josta id toimisi osoitteen idnä. Tämä muokkaisi myös vaikka viikottaiseksi dataksi jota tässä tapauksessa ei
            //saa suoraan apista mutta nämä voitaisiin ottaa päivittäisestä datasta ja vain laittaa viikko muotoon yksinkertaisella silmukalla tämä olisi myös ratkaisuni jos saatavilla
            //olisi vain viikottainen data
            using (WebClient wc = new WebClient())

            {
                
                var js = wc.DownloadString("https://helsinki-openapi.nuuka.cloud/api/v1.0/EnergyData/Monthly/ListByProperty?Record=LocationName&SearchString=1000%20Hakaniemen%20kauppahalli&ReportingGroup=Electricity&StartTime=2019-01-01&EndTime=2019-12-31");
                Sahko[]? tiedot = JsonSerializer.Deserialize<Sahko[]>(js);
                if (tiedot != null) return tiedot;
                Sahko[] aa  = new Sahko[0];
                return aa;

            };
            
        }
        
    }
    


}