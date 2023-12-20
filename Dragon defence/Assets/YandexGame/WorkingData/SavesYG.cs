
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        //// "Технические сохранения" для работы плагина (Не удалять) ////
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        //// Ваши сохранения ////

        // Данные для статистики игрока
        public int totalScore = 0; // = money

        // Данные по настройкам
        public CharacterType playerCharacterType = CharacterType.Male; // про это можно забыть, но код оставить
        // public ... masterVolume;
        // public ... musicVolume;
        // public ... sfxVolume;

        // Данные по прогрессу игрока
        // public ... roadMapStep;
        // public ... playerInventory;
        // public ... playerEquipedItems;

        //// Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны ////


        //// Вы можете выполнить какие то действия при загрузке сохранений ////
        public SavesYG()
        {

        }
    }
}
