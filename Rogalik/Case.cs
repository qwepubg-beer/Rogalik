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
            double oldHP = MainStaticClass.hero.HP;
            MainStaticClass.hero.HP = Math.Min(MainStaticClass.hero.HP + healAmount, MainStaticClass.hero.MaxHP);

            double actualHeal = MainStaticClass.hero.HP - oldHP;
            string message = $"💚 Зелье исцеления! Восстановлено {actualHeal:F0} HP";

            MainStaticClass.logs.Add(message);
            return message;
        }

        private static string GetRandomItem()
        {
            // Получаем случайный предмет из списка Weapons.Items
            int itemIndex = GetValue(Weapons.Items.Count);
            Item foundItem = Weapons.Items[itemIndex];

            // Создаем копию предмета
            Item newItem = CreateItemCopy(foundItem);

            string message = "";

            // Обработка в зависимости от типа предмета
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
                // Обычный предмет (не оружие и не броня)
                MainStaticClass.hero.Items.Add(newItem);
                message = $"📦 Вы нашли: {newItem.Name}";
            }

            MainStaticClass.logs.Add(message);
            return message;
        }

        private static string HandleWeaponDrop(Weapons.Weapon newWeapon)
        {
            // Проверяем, есть ли у героя текущее оружие
            if (MainStaticClass.hero.Damage != null)
            {
                // Спрашиваем, хочет ли игрок заменить оружие
                MessageBoxResult result = MessageBox.Show(
                    $"Найдено новое оружие: {newWeapon.Name} (Урон: {newWeapon.Damage})\n\n" +
                    $"Текущее оружие: {MainStaticClass.hero.Damage.Name} (Урон: {MainStaticClass.hero.Damage.Damage})\n\n" +
                    $"Заменить?",
                    "Новое оружие",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    // Сохраняем старое оружие в инвентарь
                    if (MainStaticClass.hero.Damage != null)
                    {
                        MainStaticClass.hero.Items.Add(MainStaticClass.hero.Damage);
                    }

                    // Экипируем новое оружие
                    MainStaticClass.hero.Damage = newWeapon;
                    return $"⚔️ Вы экипировали {newWeapon.Name} (Урон: {newWeapon.Damage})";
                }
                else
                {
                    // Добавляем в инвентарь
                    MainStaticClass.hero.Items.Add(newWeapon);
                    return $"📦 {newWeapon.Name} добавлен в инвентарь";
                }
            }
            else
            {
                // У героя нет оружия - автоматически экипируем
                MainStaticClass.hero.Damage = newWeapon;
                return $"⚔️ Вы получили {newWeapon.Name} (Урон: {newWeapon.Damage})";
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
                    return $"🛡️ Вы экипировали {newArmor.Name} (Защита: {newArmor.Protection})";
                }
                else
                {
                    // Добавляем в инвентарь
                    MainStaticClass.hero.Items.Add(newArmor);
                    return $"📦 {newArmor.Name} добавлен в инвентарь";
                }
            }
            else
            {
                // У героя нет брони - автоматически экипируем
                MainStaticClass.hero.Protection = newArmor;
                return $"🛡️ Вы получили {newArmor.Name} (Защита: {newArmor.Protection})";
            }
        }

        private static Item CreateItemCopy(Item original)
        {
            if (original is Weapons.Weapon weapon)
            {
                return new Weapons.Weapon(weapon.Name, weapon.Damage, weapon.Splash);
            }
            else if (original is Weapons.Armor armor)
            {
                return new Weapons.Armor(armor.Name, armor.Protection);
            }
            else
            {
                return new Item(original.Name);
            }
        }

    }
}
