using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static Rogalik.Battle;
using static Rogalik.Rand;
using static Rogalik.Weapons;
namespace Rogalik
{
    internal class Case
    {
        public static string Spin()
        {
            // 40% шанс на зелье исцеления
            if (GetChance(0.40))
            {
                return HealingPotion();
            }
            else
            {
                return GetRandomItem();
            }
        }

        private static string HealingPotion()
        {
            // Исцеляем героя на 50% от максимального здоровья
            double healAmount = MainStaticClass.hero.MaxHP * 0.5;
            double oldHP = MainStaticClass.hero.MaxHP;
            MainStaticClass.hero.HP = MainStaticClass.hero.MaxHP;
            string message = $"Зелье исцеления! Восстановлено HP";
            MainStaticClass.logs.Add(message);
            return message;
        }

        private static string GetRandomItem()
        {
            int itemIndex = GetValue(Items.Count);
            Item foundItem = Items[itemIndex];
            Item newItem = CreateItemCopy(foundItem);
            string message = "";
            if (newItem is Weapons.Weapon newWeapon)
            {
                message = HandleWeaponDrop(newWeapon);
            }
            else if (newItem is Weapons.Armor newArmor)
            {
                message = HandleArmorDrop(newArmor);
            }
            else
            {
                MainStaticClass.hero.Items.Add(newItem);
                message = $"Вы нашли: {newItem.Name}";
            }
            MainStaticClass.logs.Add(message);
            return message;
        }

        private static string HandleWeaponDrop(Weapons.Weapon newWeapon)
        {
            // Сохраняем текущее оружие для сравнения
            var currentWeapon = MainStaticClass.hero.Damage;

            // Используем Dispatcher для асинхронного показа MessageBox
            System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                if (currentWeapon != null)
                {
                    MessageBoxResult result = MessageBox.Show(
                        $"Найдено новое оружие: {newWeapon.Name} (Урон: {newWeapon.Damage})\n\n" +
                        $"Текущее оружие: {currentWeapon.Name} (Урон: {currentWeapon.Damage})\n\n" +
                        $"Заменить?",
                        "Новое оружие",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);

                    // Обработка результата
                    if (result == MessageBoxResult.Yes)
                    {
                        // Сохраняем старое оружие в инвентарь
                        if (currentWeapon != null)
                        {
                            MainStaticClass.hero.Items.Add(currentWeapon);
                        }

                        // Экипируем новое оружие
                        MainStaticClass.hero.Damage = newWeapon;
                        MainStaticClass.logs.Add($"⚔️ Вы экипировали {newWeapon.Name} (Урон: {newWeapon.Damage})");
                    }
                    else
                    {
                        // Добавляем в инвентарь
                        MainStaticClass.hero.Items.Add(newWeapon);
                        MainStaticClass.logs.Add($"📦 {newWeapon.Name} добавлен в инвентарь");
                    }

                    // Обновляем UI после выбора
                    UpdateUI();
                }
            }), System.Windows.Threading.DispatcherPriority.Background);

            // Возвращаем временное сообщение
            return "🔍 Выбор оружия...";
        }

        private static void UpdateUI()
        {
            // Находим открытую страницу Game и обновляем её
            var mainWindow = System.Windows.Application.Current.MainWindow;
            var frame = mainWindow?.Content as System.Windows.Controls.Frame;
            if (frame?.Content is Pages.Game gamePage)
            {
                gamePage.Dispatcher.BeginInvoke(new Action(() =>
                {
                    gamePage.RefreshUI();
                }));
            }
        }

        private static string HandleArmorDrop(Weapons.Armor newArmor)
        {
            // Проверяем, есть ли у героя текущая броня
            if (MainStaticClass.hero.Protection != null)
            {
                // Спрашиваем, хочет ли игрок заменить броню
                MessageBoxResult result = MessageBox.Show(
                    $"Найдена новая броня: {newArmor.Name} (Защита: {newArmor.Protection})\n\n" +
                    $"Текущая броня: {MainStaticClass.hero.Protection.Name} (Защита: {MainStaticClass.hero.Protection.Protection})\n\n" +
                    $"Заменить?",
                    "Новая броня",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    // Сохраняем старую броню в инвентарь
                    if (MainStaticClass.hero.Protection != null)
                    {
                        MainStaticClass.hero.Items.Add(MainStaticClass.hero.Protection);
                    }

                    // Экипируем новую броню
                    MainStaticClass.hero.Protection = newArmor;
                    return $"Вы экипировали {newArmor.Name} (Защита: {newArmor.Protection})";
                }
                else
                {
                    // Добавляем в инвентарь
                    MainStaticClass.hero.Items.Add(newArmor);
                    return $"{newArmor.Name} добавлен в инвентарь";
                }
            }
            else
            {
                // У героя нет брони - автоматически экипируем
                MainStaticClass.hero.Protection = newArmor;
                return $"Вы получили {newArmor.Name} (Защита: {newArmor.Protection})";
            }
        }

        private static Item CreateItemCopy(Item original)
        {
            if (original is Weapon weapon)
            {
                return new Weapon(weapon.Name, weapon.Damage, weapon.Splash);
            }
            else if (original is Armor armor)
            {
                return new Armor(armor.Name, armor.Protection);
            }
            else
            {
                return new Item(original.Name);
            }
        }

    }
}
