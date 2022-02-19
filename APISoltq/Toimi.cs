using System.Net;
using System.Text.Json;
using System;
using System.IO;
using System.Text;

namespace APISoltq
{
    public class Toimi
    {
        //Luo uuden .csv tiedoston ja jonka se sitten täyttää tällä hetkellä luo uuden kansion vain jokaiselle alkavalle minuutille
        public static void Kirjoita(Sahko[] tiedot)
        {
            string fileName = "CSV" + DateTime.Now.ToString("yyyyMMddHHmm") + ".csv";

            //laittaa polun data kansioon
            string path = Path.Combine(Environment.CurrentDirectory, @"Data", fileName);
            try
            {   //Luo uuden tiedoston tai kirjoittaa vanhan päälle
                using (FileStream fs = File.Create(path))
                {
                    for (int i = 0; i < tiedot.Length; i++)
                    {
                        Byte[] info = new UTF8Encoding(true).GetBytes(tiedot[i].timestamp.ToString("yyyyMM") + "\n" + tiedot[i].value + "\n" + tiedot[i].unit + "\n");
                        fs.Write(info, 0, info.Length);

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
