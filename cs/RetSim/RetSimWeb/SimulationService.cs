using RetSim;
using RetSim.Data;
using RetSim.Items;
using RetSim.Simulation;
using RetSim.Simulation.Tactics;
using RetSim.Units.Enemy;
using RetSim.Units.Player;
using RetSim.Units.Player.Static;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace RetSimWeb
{
    public class SimulationService
    {
        public HttpClient HttpClient { get; }

        public SimulationService(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public async Task<bool> InitAsync(string httpBaseAdress)
        {
            HttpClient.BaseAddress = new Uri(httpBaseAdress);
            Items.Initialize(await LoadWeponDataAsync(), await LoadArmorDataAsync(), await LoadSetDataAsync(), await LoadGemDataAsync(), await LoadMetaGemDataAsync(), await LoadEnchantDataAsync());
            return await Task.Run(() => true);
        }

        public float Run()
        {
            var localDps = 0f;
			var iterations = 10000;
            Console.WriteLine(System.DateTime.Now.ToString());
			for (int i = 0; i < iterations; i++)
			{
				FightSimulation fight = new(new Player("Brave Hero", Collections.Races["Human"], new() { Weapon = Items.Weapons[28429] }, new()), new Enemy(Collections.Bosses[17]), new EliteTactic(), null, null, null, 180000, 190000);
                fight.Run();

				localDps += fight.CombatLog.DPS;
			}
            Console.WriteLine(System.DateTime.Now.ToString());

            return localDps / iterations;
		}

        public async Task<List<EquippableWeapon>> LoadWeponDataAsync()
        {
            return JsonSerializer.Deserialize<List<EquippableWeapon>>(await HttpClient.GetStringAsync("/data/weapons.json"));
        }

        public async Task<List<EquippableItem>> LoadArmorDataAsync()
        {
            return JsonSerializer.Deserialize<List<EquippableItem>>(await HttpClient.GetStringAsync("/data/armor.json"));
        }

        public async Task<List<ItemSet>> LoadSetDataAsync()
        {
            return JsonSerializer.Deserialize<List<ItemSet>>(await HttpClient.GetStringAsync("/data/sets.json"));
        }

        public async Task<List<Gem>> LoadGemDataAsync()
        {
            return JsonSerializer.Deserialize<List<Gem>>(await HttpClient.GetStringAsync("/data/gems.json"));
        }

        public async Task<List<MetaGem>> LoadMetaGemDataAsync()
        {
            return JsonSerializer.Deserialize<List<MetaGem>>(await HttpClient.GetStringAsync("/data/metaGems.json"));
        }

        public async Task<List<Enchant>> LoadEnchantDataAsync()
        {
            return JsonSerializer.Deserialize<List<Enchant>>(await HttpClient.GetStringAsync("/data/enchants.json"));
        }
    }
}
