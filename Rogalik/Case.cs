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
                    if (result == MessageBoxResult.Yes)
                    {
                        MainStaticClass.hero.Damage = newWeapon;
                        MainStaticClass.logs.Add($"Вы экипировали {newWeapon.Name} (Урон: {newWeapon.Damage})");
                    }
                    else
                    {
                        MainStaticClass.hero.Items.Add(newWeapon);
                        MainStaticClass.logs.Add($"Вы выбросили  {newWeapon.Name}");
                    }
                    UpdateUI();
                }
            }), System.Windows.Threading.DispatcherPriority.Background);
            return "Выбор оружия...";
        }

        private static void UpdateUI()
        {
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
            if (MainStaticClass.hero.Protection != null)
            {
                MessageBoxResult result = MessageBox.Show(
                    $"Найдена новая броня: {newArmor.Name} (Защита: {newArmor.Protection})\n\n" +
                    $"Текущая броня: {MainStaticClass.hero.Protection.Name} (Защита: {MainStaticClass.hero.Protection.Protection})\n\n" +
                    $"Заменить?",
                    "Новая броня",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    MainStaticClass.hero.Protection = newArmor;
                    return $"Вы экипировали {newArmor.Name} (Защита: {newArmor.Protection})";
                }
                else
                {
                    return $"Вы выбросили {newArmor.Name}";
                }
            }
            else
            {
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
