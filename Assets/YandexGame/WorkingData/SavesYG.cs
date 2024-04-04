
using System.Collections.Generic;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Тестовые сохранения для демо сцены
        // Можно удалить этот код, но тогда удалите и демо (папка Example)
                              // Можно задать полям значения по умолчанию
        public string newPlayerName = "Hello!";
        public bool[] openLevels = new bool[3];

        // Ваши сохранения
        public int Money = 200;
        public List <Zombiee> ZombieSaveYG = new List <Zombiee>();
        public int WaveZombie = 1;

        public int[] MissionCount = new int[4];
        public int[] MissioncountMax = new int[] {2,3,4,5};
        public int[] ReawrdMoney = new int[] {10,20,25,40};

        // Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны


        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            // Допустим, задать значения по умолчанию для отдельных элементов массива
            //MissioncountMax[0] = 10;
            //MissioncountMax[1] = 20;
            //MissioncountMax[2] = 25;
            //MissioncountMax[3] = 40;
            openLevels[1] = true;
        }
    }
}
