using System.Diagnostics.Contracts;
using System.Text;
using ConsoleAdventure.DataTypes;
using ConsoleAdventure.Items.Armors;
using ConsoleAdventure.Utilities;

namespace ConsoleAdventure.Items;

public abstract class Armor : Equippable
{
    public override string Name => (Quality == 0) ? _name : $"{ArmorQuality.ToString()} {_name}";
    public override string StatsFull => GetFullStats();

    public int Protection { get; }
    private readonly Range<int> _protectionRange;
    public ArmorQuality ArmorQuality => (ArmorQuality)Quality;

    //constructor for random protection
    protected Armor(string name, Range<int> protectionRange, EquipSlot equipSlot) : base(name, equipSlot)
    {
        _protectionRange = protectionRange;

        //generate protection
        var rng = new Random();
        float firstDice = rng.NextSingle();
        float secondDice = rng.NextSingle();
        float rngResult = (firstDice + secondDice) / 2f;
        Protection = (int)MathUtilities.Remap(rngResult, 0f, 1f, _protectionRange.Minimum, _protectionRange.Maximum + 1);
    }
    
    private string GetFullStats()
    {
        var fullStats = new StringBuilder();

        fullStats.AppendLine($"Name: {Name}");
        fullStats.AppendLine($"Description: {StatsShort}");
        fullStats.AppendLine($"Protection: {Protection}");
        fullStats.AppendLine($"Slot: {EquipSlot.ToString()}");
        fullStats.AppendLine($"Value: {GoldValue}");
        fullStats.AppendLine($"Equipped: {(IsEquipped ? "yes" : "no")}");

        return fullStats.ToString();
    }
    
    protected override int CalculateGoldValue()
    {
        return (int)MathF.Ceiling(Protection * Constants.ArmorGoldPerProtection);
    }

    protected override int CalculateQuality()
    {
        //from -3 (bad) to 3 (legendary). 0 is normal quality
        if (_protectionRange.Minimum == _protectionRange.Maximum) return 0;
        
        float qualityPercent = 1f / (_protectionRange.Maximum - _protectionRange.Minimum) * (Protection - _protectionRange.Minimum);
        float quality = MathUtilities.Remap(qualityPercent, 0f, 1f, -3.45f, 3.45f);
        return (int)MathF.Round(quality);
    }
    
    [Pure]
    public static Armor GenerateRandomArmor()
    {
        Func<Armor>[] possibleArmors =
        {
            //leather
            () => new LeatherBracers(),
            () => new LeatherBoots(),
            () => new LeatherGloves(),
            () => new LeatherHelmet(),
            () => new LeatherGreaves(),
            () => new LeatherTorso(),
            
            //chain
            () => new ChainBracers(),
            () => new ChainBoots(),
            () => new ChainGloves(),
            () => new ChainHelmet(),
            () => new ChainGreaves(),
            () => new ChainTorso(),
            
            //scale
            () => new ScaleBracers(),
            () => new ScaleBoots(),
            () => new ScaleGloves(),
            () => new ScaleHelmet(),
            () => new ScaleGreaves(),
            () => new ScaleTorso(),
            
            //plate
            () => new PlateBracers(),
            () => new PlateBoots(),
            () => new PlateGloves(),
            () => new PlateHelmet(),
            () => new PlateGreaves(),
            () => new PlateTorso(),
            
        };

        var rng = new Random();
        int randomIndex = rng.Next(possibleArmors.Length);
        return possibleArmors[randomIndex]();
    }
}