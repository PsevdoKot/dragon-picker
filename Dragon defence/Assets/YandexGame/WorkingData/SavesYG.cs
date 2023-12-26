
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
        public int totalScore = 10; // = money

        // Данные по настройкам
        public CharacterType playerCharacterType = CharacterType.Male; // про это можно забыть, но код оставить
        public float masterVolume = -20f; // от -40 до 0
        public float musicVolume = 0; // от -40 до 0
        public float sfxVolume = 0; // от -40 до 0

        // Данные по прогрессу игрока
        public int roadMapStep = 0; // отчёт от 0
        public int waterTotemManaCostUpgrade = -1; // -1 = без улучшений
        public int waterTotemMaxHPUpgrade = -1; // при уменьшении максимального количества улучшений в дальнейших апдейтах может
                                                // произойти баг, когда у игрока улучшений больше, чем существует в PlayerCharsManager.totemUpgradesData
        public int waterTotemTimeBetweenActionsUpgrade = -1;
        public int waterTotemManaRegenAmountUpgrade = -1;

        public int fireTotemManaCostUpgrade = -1;
        public int fireTotemMaxHPUpgrade = -1;
        public int fireTotemTimeBetweenActionsUpgrade = -1;
        public int totemFireballDamageUpgrade = -1;
        public int totemFireballSpeedUpgrade = -1;

        public int airTotemManaCostUpgrade = -1;
        public int airTotemMaxHPUpgrade = -1;
        public int airTotemTimeBetweenActionsUpgrade = -1;
        public int shieldDurationUpgrade = -1;
        public int shieldAmountUpgrade = -1;

        public int earthTotemManaCostUpgrade = -1;
        public int earthTotemMaxHPUpgrade = -1;
        public int earthTotemTimeBetweenActionsUpgrade = -1;
        public int slowDownDurationUpgrade = -1;
        public int slowDownStrengthUpgrade = -1;


        //// Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны ////


        //// Вы можете выполнить какие то действия при загрузке сохранений ////
        public SavesYG()
        {

        }
    }
}
