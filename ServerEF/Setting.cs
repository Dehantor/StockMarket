using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ServerEF
{
    /// <summary>
    /// Класс для работы с настройками
    /// </summary>
    public class Setting
    {
        public static Setting set = new Setting();
        int _source=1;//источник
        int _time = 50000;//задержка для считывание с источника
        public int Source { get { return _source; }  }
        public int Time { get { return _time; }  }
        string fileName = "appsettings.json";
        Config config = new Config();
        /// <summary>
        /// Загрузка конфигураций с файла
        /// </summary>
        public Setting()
        {
            string json = System.IO.File.ReadAllText(fileName);
            config = JsonSerializer.Deserialize<Config>(json);
            _source = config.Sourse;
            _time = config.Time;
        }
        /// <summary>
        /// Измениние конфигурации сервера
        /// </summary>
        /// <param name="source">Источник</param>
        /// <param name="time">время задержки</param>
        public void SetSetting(int source,int time)
        {
            config.Sourse = source;
            config.Time = time;
            string json=JsonSerializer.Serialize<Config>(config);
            System.IO.File.WriteAllText(fileName,json);
            _source = source;
            _time = time;
        }
        /// <summary>
        /// Для десерелизации файла
        /// </summary>
        class Config {
           public  int Sourse { get; set; }
           public  int Time { get; set; }
        }
            
    }

}
