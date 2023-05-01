using System;
using BepInEx;
using UnityEngine;
using UnityEngine.UI;
using Utilla;
using static System.Net.Mime.MediaTypeNames;
using Text = UnityEngine.UI.Text;

namespace SonnyMods
{
	/// <summary>
	/// This is your mod's main class.
	/// </summary>

	/* This attribute tells Utilla to look for [ModdedGameJoin] and [ModdedGameLeave] */
	[ModdedGamemode]
	[BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
	[BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
	public class Plugin : BaseUnityPlugin
	{
		bool inRoom; 
		public static string newMOTDmessage;

		void Start()
		{
			/* A lot of Gorilla Tag systems will not be set up when start is called /*
			/* Put code in OnGameInitialized to avoid null references */

			Utilla.Events.GameInitialized += OnGameInitialized;
		}

		void OnEnable()
		{
			
			/* Set up your mod here */
			/* Code here runs at the start and whenever your mod is enabled*/
			if (inRoom == true)
			{
                GameObject.Find("Level/forest/campgroundstructure/scoreboard").SetActive(false);
			}
			else
			{
                GameObject.Find("Level/forest/campgroundstructure/scoreboard").SetActive(true);
                newMOTDmessage = "Mod loaded and enabled";
            }
			newMOTDmessage = "Mod loaded and enabled";
			GameObject.Find("motdtext").GetComponent<Text>().color = Color.yellow;
			HarmonyPatches.ApplyHarmonyPatches();
			
		}

		void OnDisable()
		{
            /* Undo mod setup here */
            /* This provides support for toggling mods with ComputerInterface, please implement it :) */
            /* Code here runs whenever your mod is disabled (including if it disabled on startup)*/
            GameObject.Find("Level/forest/campgroundstructure/scoreboard").SetActive(true);
            newMOTDmessage = "Mod loaded but not enabled";
			GameObject.Find("motdtext").GetComponent<Text>().color = Color.red;
			HarmonyPatches.RemoveHarmonyPatches();
		}

		void OnGameInitialized(object sender, EventArgs e)
		{
		}

		void FixedUpdate()
        {
			if (GameObject.Find("motdtext").GetComponent<Text>().text != newMOTDmessage)
			{
                GameObject.Find("motdtext").GetComponent<Text>().text = newMOTDmessage;
            }
        }

		void Update()
		{
			/* Code here runs every frame when the mod is enabled */
		}

		/* This attribute tells Utilla to call this method when a modded room is joined */
		[ModdedGamemodeJoin]
		public void OnJoin(string gamemode)
		{
			/* Activate your mod here */
			/* This code will run regardless of if the mod is enabled*/
			inRoom = true;
		}

		/* This attribute tells Utilla to call this method when a modded room is left */
		[ModdedGamemodeLeave]
		public void OnLeave(string gamemode)
		{
			/* Deactivate your mod here */
			/* This code will run regardless of if the mod is enabled*/
			GameObject.Find("Level/forest/campgroundstructure/scoreboard").SetActive(true);
			inRoom = false;
		}
	}
}
