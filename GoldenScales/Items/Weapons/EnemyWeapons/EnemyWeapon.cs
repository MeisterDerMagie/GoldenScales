//(c) copyright by Martin M. Klöckener

using ConsoleAdventure.DataTypes;

namespace ConsoleAdventure.Items.EnemyWeapons;

public abstract class EnemyWeapon : Weapon
{
    protected EnemyWeapon(string name, Range<int> baseDamageMinRange, Range<int> baseDamageMaxRange, Range<float> critChanceRange, Range<float> critMultiplierRange, Range<float> attackDurationRange) : base(name, baseDamageMinRange, baseDamageMaxRange, critChanceRange, critMultiplierRange, attackDurationRange)
    {
    }
    
    public override int GetDamage()
    {
        var rng = new Random();

        //base damage plus a fracture of the total discovered rooms, to make enemies stronger towards the end of the dungeon
        int baseDamage = rng.Next(BaseDamage.Minimum, BaseDamage.Maximum + 1);
        bool isCritical = rng.NextSingle() < CritChance;
        int critDamage = isCritical ? (int)MathF.Round(baseDamage * CritMultiplier) : baseDamage;
        int finalDamage = critDamage + (int)(Game.Singleton.TotalDiscoveredRooms / 2f);
        return finalDamage;
    }
}