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
        //T‰ll‰ hetkell‰ ottaa vain hakaniemen tiedot mutta voidaan helposti laajentaa

        public string Get(int id)
        {
            
            Sahko[] tiedot = TiedotHae(id);
            //antaa virheen jos rajapinnassa oli jotain h‰ikk‰‰
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
            //T‰ss‰ kohtaa id p‰‰tt‰isi mist‰ osoitteesta l‰hdett‰isiin etsim‰‰n tietoa mutta t‰ss‰ kohtaa olen vain hardkoodannut t‰m‰n etsim‰‰n oikean datan
            //mutta oikeasti laittaisin todenn‰kˆisesti taulukon josta id toimisi osoitteen idn‰.
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