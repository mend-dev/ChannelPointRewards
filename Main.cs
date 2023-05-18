using Il2CppAssets.Scripts.Models.Rounds;
using Il2CppAssets.Scripts.Unity.Bridge;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppSystem.Collections.Generic;
using MelonLoader;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Random = System.Random;

namespace ChannelPointRewards {
    public class Main : MelonMod {

        // test

        private double moneyFreeze = 0;
        private bool moneyFrozen = false;

        [System.Obsolete]
        public override void OnApplicationStart() {
            base.OnApplicationStart();
            //Logger.Log("Voice Activation Has Loaded");
        }

        public override void OnUpdate() {
            base.OnUpdate();

            if (InGame.instance != null) {
                if (InGame.instance.bridge != null) {
                    if (moneyFrozen) {
                        InGame.instance.bridge.SetCash(moneyFreeze);
                    }

                    if (Input.GetKeyDown(KeyCode.Keypad0)) {
                        InGame.instance.bridge.SpawnBloons(GetBloomEmissionArray("PinkCamo", 50, 5), 40, 0);
                        int bloonValue = (int)((BloonValues)Enum.Parse(typeof(BloonValues), "Pink"));
                        int equivalentMoney = (bloonValue * 50);
                        InGame.instance.bridge.SetCash(InGame.instance.bridge.GetCash() - equivalentMoney);
                        moneyFreeze -= equivalentMoney;
                    } else if (Input.GetKeyDown(KeyCode.Keypad1)) {
                        InGame.instance.bridge.SpawnBloons(GetBloomEmissionArray("Purple", 50, 10), 40, 0);
                        int bloonValue = (int)((BloonValues)Enum.Parse(typeof(BloonValues), "Purple"));
                        int equivalentMoney = (bloonValue * 50);
                        InGame.instance.bridge.SetCash(InGame.instance.bridge.GetCash() - equivalentMoney);
                        moneyFreeze -= equivalentMoney;
                    } else if (Input.GetKeyDown(KeyCode.Keypad2)) {
                        InGame.instance.bridge.SpawnBloons(GetBloomEmissionArray("Lead", 50, 15), 40, 0);
                        int bloonValue = (int)((BloonValues)Enum.Parse(typeof(BloonValues), "Lead"));
                        int equivalentMoney = (bloonValue * 50);
                        InGame.instance.bridge.SetCash(InGame.instance.bridge.GetCash() - equivalentMoney);
                        moneyFreeze -= equivalentMoney;
                    } else if (Input.GetKeyDown(KeyCode.Keypad3)) {
                        Il2CppSystem.Collections.Generic.List<TowerToSimulation> tts = InGame.instance.bridge.GetAllTowers();
                        Random rnd = new Random();
                        int tower = rnd.Next(tts.Count);
                        float value = tts[tower].sellFor;
                        InGame.instance.bridge.SellTower(tts[tower].Id);
                        moneyFreeze -= value;
                        InGame.instance.bridge.SetCash(InGame.instance.bridge.GetCash() - value);
                    } else if (Input.GetKeyDown(KeyCode.Keypad4)) {
                        moneyFreeze -= ((InGame.instance.bridge.GetCurrentRound() + 1) * 100);
                        InGame.instance.bridge.SetCash(InGame.instance.bridge.GetCash() - ((InGame.instance.bridge.GetCurrentRound() + 1) * 50));
                    } else if (Input.GetKeyDown(KeyCode.Keypad5)) {
                        moneyFreeze = InGame.instance.bridge.GetCash();
                        moneyFrozen = true;
                        Task task = new Task(MoneyFreezeCooldown);
                        task.Start();
                    } else if (Input.GetKeyDown(KeyCode.Keypad6)) {
                        InGame.instance.lockTowerUpgrades = true;
                        Task task = new Task(LockUpgradesCooldown);
                        task.Start();
                    } else if (Input.GetKeyDown(KeyCode.Keypad7)) {
                        InGame.instance.lockTowerPurchases = true;
                        Task task = new Task(LockPurchasingCooldown);
                        task.Start();
                    } else if (Input.GetKeyDown(KeyCode.Keypad8)) {
                        InGame.instance.lockAbilities = true;
                        Task task = new Task(LockAbilitiesCooldown);
                        task.Start();
                    } else if (Input.GetKeyDown(KeyCode.Keypad9)) {
                    }
                }
            }
        }

        private void MoneyFreezeCooldown() {
            Thread.Sleep(30 * 1000);
            MelonLogger.Msg("Unfreeze Money");
            moneyFrozen = false;
        }

        private void LockUpgradesCooldown() {
            Thread.Sleep(60 * 1000);
            MelonLogger.Msg("Reenable Upgrades");
            InGame.instance.lockTowerUpgrades = false;
        }

        private void LockPurchasingCooldown() {
            Thread.Sleep(120 * 1000);
            MelonLogger.Msg("Reenable Purchases");
            InGame.instance.lockTowerPurchases = false;
        }

        private void LockAbilitiesCooldown() {
            Thread.Sleep(60 * 1000);
            MelonLogger.Msg("Reenable Abilities");
            InGame.instance.lockAbilities = false;
        }

        private void DisableTowerCooldown() {
            Thread.Sleep(240 * 1000);
            moneyFrozen = false;
        }

        public Il2CppReferenceArray<BloonEmissionModel> GetBloomEmissionArray(String bloon, int amount, int spawnTime) {
            Il2CppReferenceArray<BloonEmissionModel> bme = new Il2CppReferenceArray<BloonEmissionModel>(amount);
            for (int i = 0; i < bme.Length; i++) {
                bme[i] = new BloonEmissionModel(bloon, i * spawnTime, bloon);
            }
            return bme;
        }

        public enum BloonValues {
            red = 1,
            blue = 2,
            green = 3,
            yellow = 4,
            pink = 5,
            black = 11,
            white = 11,
            purple = 11,
            zebra = 23,
            lead = 23,
            rainbow = 47,
            ceramic = 95,
            moab = 381,
            bfb = 1525,
            zomg = 6101,
            ddt = 381,
            bad = 13346,
            testbloon = 0
        }
    }
}
