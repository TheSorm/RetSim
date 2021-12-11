using RetSim.Data;
using RetSim.Items;
using RetSim.Simulation;
using RetSim.Simulation.Tactics;
using RetSim.Spells;
using RetSim.Units.Enemy;
using RetSim.Units.Player;
using RetSim.Units.Player.Static;
using RetSim.Units.UnitStats;
using RetSimDesktop.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;

namespace RetSimDesktop
{
    /// <summary>
    /// Interaction logic for StatPanel.xaml
    /// </summary>
    public partial class StatPanel : UserControl
    {
        public StatPanel()
        {
            this.DataContextChanged += (o, e) =>
            {
                if (DataContext is RetSimUIModel retSimUIModel)
                {
                    retSimUIModel.SelectedGear.PropertyChanged += Model_PropertyChanged;
                    retSimUIModel.SelectedTalents.PropertyChanged += Model_PropertyChanged;
                    retSimUIModel.PlayerSettings.PropertyChanged += Model_PropertyChanged;
                    retSimUIModel.EncounterSettings.PropertyChanged += Model_PropertyChanged;

                    Model_PropertyChanged(this, new PropertyChangedEventArgs(""));
                }
            };
            InitializeComponent();
        }

        private void Model_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (DataContext is RetSimUIModel retSimUIModel)
            {
                var equipment = retSimUIModel.SelectedGear.GetEquipment();
                var player = new Player("Brave Hero", Collections.Races[retSimUIModel.PlayerSettings.SelectedRace.ToString()], ShattrathFaction.Aldor,
                    equipment, retSimUIModel.SelectedTalents.GetTalentList());
                FightSimulation fight = new(player, new Enemy(Collections.Bosses[retSimUIModel.EncounterSettings.EncounterID]), new EliteTactic(), new List<Spell>(), new List<Spell>(), new List<Spell>(), 0, 0);

                StatPanelBoxHeader.Content = "Level 70 " + Collections.Races[retSimUIModel.PlayerSettings.SelectedRace.ToString()].Name + " Paladin";

                Stamina.Content = player.Stats[StatName.Stamina].Value;
                Health.Content = player.Stats[StatName.Health].Value;
                Intellect.Content = player.Stats[StatName.Intellect].Value;
                Mana.Content = player.Stats[StatName.Mana].Value;
                ManaPer5.Content = player.Stats[StatName.ManaPer5].Value;
                Strength.Content = player.Stats[StatName.Strength].Value;
                AttackPower.Content = player.Stats[StatName.AttackPower].Value;
                Agility.Content = player.Stats[StatName.Agility].Value;
                ArmorPenetration.Content = player.Stats[StatName.ArmorPenetration].Value;
                WeaponDamage.Content = player.Stats[StatName.WeaponDamage].Value;
                SpellPower.Content = player.Stats[StatName.SpellPower].Value;

                CritPercentage.Content = player.Stats[StatName.CritChance].Value.ToString("0.##") + "%";
                HitPercentage.Content = player.Stats[StatName.HitChance].Value.ToString("0.##") + "%";
                HastePercentage.Content = player.Stats[StatName.Haste].Value.ToString("0.##") + "%";
                ExpertisePercentage.Content = player.Stats[StatName.Expertise].Value.ToString("0");

                var staminaBase = $"Base: {player.Stats[StatName.Stamina].Race}";
                var staminaBonus = $"Bonus: {player.Stats[StatName.Stamina].Gear + player.Stats[StatName.Stamina].Bonus}";
                var staminaModifier = $"Multiplier: {player.Stats[StatName.Stamina].Modifier * 100 - 100}% (+{player.Stats[StatName.Stamina].Value - player.Stats[StatName.Stamina].Value / player.Stats[StatName.Stamina].Modifier:0})";

                Stamina.ToolTip = new ToolTip { Content = $"{staminaBase}\n{staminaBonus}\n{staminaModifier}" };

                var healthBase = $"Base: {player.Stats[StatName.Health].Permanent}";
                var healthBonus = $"Bonus: {player.Stats[StatName.Health].Gear + player.Stats[StatName.Health].Bonus}";
                var healthStamina = $"Stamina Bonus: {player.Stats[StatName.Health].SupportValue}";

                Health.ToolTip = new ToolTip { Content = $"{healthBase}\n{healthBonus}\n{healthStamina}" };

                var intellectBase = $"Base: {player.Stats[StatName.Intellect].Race}";
                var intellectBonus = $"Bonus: {player.Stats[StatName.Intellect].Gear + player.Stats[StatName.Intellect].Bonus}";
                var intellectModifier = $"Multiplier: {player.Stats[StatName.Intellect].Modifier * 100 - 100}% (+{player.Stats[StatName.Intellect].Value - player.Stats[StatName.Intellect].Value / player.Stats[StatName.Intellect].Modifier:0})";

                Intellect.ToolTip = new ToolTip { Content = $"{intellectBase}\n{intellectBonus}\n{intellectModifier}" };

                var manaBase = $"Base: {player.Stats[StatName.Mana].Permanent}";
                var manaBonus = $"Bonus: {player.Stats[StatName.Mana].Gear + player.Stats[StatName.Mana].Bonus}";
                var manaIntellect = $"Intellect Bonus: {player.Stats[StatName.Mana].SupportValue:0}";

                Mana.ToolTip = new ToolTip { Content = $"{manaBase}\n{manaBonus}\n{manaIntellect}" };

                var manaPer5Bonus = $"Mana regenerated over the fight's duration: ~{((retSimUIModel.EncounterSettings.MinFightDuration + retSimUIModel.EncounterSettings.MaxFightDuration) / 10000) * player.Stats[StatName.ManaPer5].Value}";

                ManaPer5.ToolTip = new ToolTip { Content = $"{manaPer5Bonus}" };

                var strengthBase = $"Base: {player.Stats[StatName.Strength].Race}";
                var strengthBonus = $"Bonus: {player.Stats[StatName.Strength].Gear + player.Stats[StatName.Strength].Bonus}";
                var strengthModifier = $"Multiplier: {player.Stats[StatName.Strength].Modifier * 100 - 100}% (+{player.Stats[StatName.Strength].Value - player.Stats[StatName.Strength].Value / player.Stats[StatName.Strength].Modifier:0})";

                Strength.ToolTip = new ToolTip { Content = $"{strengthBase}\n{strengthBonus}\n{strengthModifier}" };

                var attackPowerBase = $"Base: {player.Stats[StatName.AttackPower].Race}";
                var attackPowerBonus = $"Bonus: {player.Stats[StatName.AttackPower].Gear + player.Stats[StatName.AttackPower].Bonus}";
                var attackPowerStrength = $"Strength Bonus: {player.Stats[StatName.AttackPower].SupportValue}";
                var attackPowerModifier = $"Multiplier: {player.Stats[StatName.AttackPower].Modifier * 100 - 100}% (+{(int)(player.Stats[StatName.AttackPower].Value - player.Stats[StatName.AttackPower].Value / player.Stats[StatName.AttackPower].Modifier)})";

                AttackPower.ToolTip = new ToolTip { Content = $"{attackPowerBase}\n{attackPowerBonus}\n{attackPowerStrength}\n{attackPowerModifier}" };

                var agilityBase = $"Base: {player.Stats[StatName.Agility].Race}";
                var agilityBonus = $"Bonus: {player.Stats[StatName.Agility].Gear + player.Stats[StatName.Agility].Bonus}";
                var agilityModifier = $"Multiplier: {player.Stats[StatName.Agility].Modifier * 100 - 100}% (+{(int)(player.Stats[StatName.Agility].Value - player.Stats[StatName.Agility].Value / player.Stats[StatName.Agility].Modifier)})";

                Agility.ToolTip = new ToolTip { Content = $"{agilityBase}\n{agilityBonus}\n{agilityModifier}" };

                var critChanceBase = $"Base: {player.Stats[StatName.CritChance].Race}%";
                var critChanceBonus = $"Bonus: {player.Stats[StatName.CritChance].Gear + player.Stats[StatName.CritChance].Bonus}%";
                var critChanceAgility = $"Agility Bonus: {player.Stats[StatName.CritChance].SupportValue:0.##}%";
                var critRating = $"Rating: {player.Stats[StatName.CritRating].Value} (+{player.Stats[StatName.CritChance].RatingValue:0.##}%)";
                var critEffective = $"Effective crit chance against bosses: {player.Stats.EffectiveCritChance:0.##}%";

                var missChance = Math.Max(player.Stats.EffectiveMissChance - fight.Enemy.Stats[StatName.IncreasedAttackerHitChance].Value, 0);
                var dodgeChance = player.Stats.EffectiveDodgeChance;
                var specialCap = 100 - missChance - dodgeChance;
                var autoCap = specialCap - RetSim.Misc.Constants.Boss.GlancingChance;

                var critResult = $"Crit caps: {autoCap:0.##}% (White) / {specialCap:0.##}% (Special)";

                CritPercentage.ToolTip = new ToolTip { Content = $"{critChanceBase}\n{critRating}\n{critChanceBonus}\n{critChanceAgility}\n\n{critResult}\n{critEffective}" };

                var hitChanceBonus = $"Bonus: {player.Stats[StatName.HitChance].Gear + player.Stats[StatName.HitChance].Bonus}%";
                var hitChanceRating = $"Rating: {player.Stats[StatName.HitRating].Value} (+{player.Stats[StatName.HitChance].RatingValue:0.##}%)";
                var hitChanceResult = $"Miss chance: {missChance:0.##}%";

                HitPercentage.ToolTip = new ToolTip { Content = $"{hitChanceRating}\n{hitChanceBonus}\n\n{hitChanceResult}" };

                var hasteBonus = $"Bonus: {player.Stats[StatName.Haste].Gear + player.Stats[StatName.Haste].Bonus}%";
                var hasteRating = $"Rating: {player.Stats[StatName.HasteRating].Value} (+{player.Stats[StatName.Haste].RatingValue:0.##}%)";
                var hasteResult = $"Weapon speed: {fight.Player.Weapon.EffectiveSpeed / 1000f} (Current) / {fight.Player.Weapon.BaseSpeed / 1000f} (Base)";

                HastePercentage.ToolTip = new ToolTip { Content = $"{hasteRating}\n{hasteBonus}\n\n{hasteResult}" };

                var expertiseBonus = $"Bonus: {player.Stats[StatName.Expertise].Gear + player.Stats[StatName.Expertise].Bonus}";
                var expertiseRating = $"Rating: {player.Stats[StatName.ExpertiseRating].Value} (+{player.Stats[StatName.Expertise].RatingValue:0.##})";
                var expertiseResult = $"{fight.Enemy.Name}'s dodge chance: {dodgeChance:0.##}%";

                ExpertisePercentage.ToolTip = new ToolTip { Content = $"{expertiseRating}\n{expertiseBonus}\n\n{expertiseResult}" };

                var armorPenResult = $"{fight.Enemy.Name}'s armor: {Math.Max(fight.Enemy.Stats[StatName.Armor].Value - player.Stats[StatName.ArmorPenetration].Value, 0):0} (Remaining) / {fight.Enemy.Stats[StatName.Armor].Permanent} (Base)";

                var baseDR = Attack.GetArmorDR(0, fight.Enemy.Stats[StatName.Armor].Value);
                var currentDR = Attack.GetArmorDR(player.Stats[StatName.ArmorPenetration].Value, fight.Enemy.Stats[StatName.Armor].Value);

                var armorPenResult2 = $"{fight.Enemy.Name}'s damage reduction: {1 - currentDR:0.##%} (Remaining) / {1 - baseDR:0.##%} (Base)";

                var armorPenResult3 = $"Armor penetration damage increase: {currentDR - baseDR:0.##%} (Flat) / {currentDR / baseDR - 1:0.##%} (Relative)";

                ArmorPenetration.ToolTip = new ToolTip { Content = $"{armorPenResult}\n{armorPenResult2}\n\n{armorPenResult3}" };

                var weaponDamageWarning = "This stat is nearly useless.";
                var weaponDamageWarning2 = "You can ignore it.";

                WeaponDamage.ToolTip = new ToolTip { Content = $"{weaponDamageWarning}\n{weaponDamageWarning2}" };


                var gemCount = Equipment.GetGemCount(equipment);

                RedGemCount.Content = gemCount[GemColor.Red];
                BlueGemCount.Content = gemCount[GemColor.Blue];
                YellowGemCount.Content = gemCount[GemColor.Yellow];


                if (equipment.Head.Socket1 != null && equipment.Head.Socket1.IsMetaGem() is MetaGem meta)
                {
                    if (meta.IsActive(gemCount[GemColor.Red], gemCount[GemColor.Blue], gemCount[GemColor.Yellow]))
                    {
                        MetaGemActive.Content = "Active";
                    }
                    else
                    {
                        MetaGemActive.Content = "Inactive";
                    }
                }
                else
                {
                    MetaGemActive.Content = "-";
                }

            }
        }
    }
}
