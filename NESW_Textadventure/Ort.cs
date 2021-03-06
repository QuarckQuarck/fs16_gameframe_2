﻿using System;
using System.Collections.Generic;
using System.Linq;

using TextAdventure;

namespace TextAdventure
{
	// Eine Implementierung des Standard Ortes
	// alle Funktionalität, die für sämtliche
	// speziellen Orte existieren, müssen hier rein
	// Klasse ist abstract, weil wir nie eine Instanz davon
	// brauchen
	public abstract class Ort
	{
		// -- Properties, die alle Orte haben sollen
		protected string name = "untitled room";
		protected string bezug = "in";
		protected string beschrieb;

		// Property für die Verknüpfungen des Raums
		// zu anderen Räumen
		Dictionary<string, Ort> verknuepfungen =
			new Dictionary<string, Ort>();

		// Property für die Dinge, die in diesem Raum
		// sind
		Dictionary<string, Ding> dinge =
			new Dictionary<string, Ding>();
		

		// -- Konstruktor des allgemeinen Ortes ohne Parameter
		public Ort ()
		{
			
		}

		// -- Konstruktor des allgemeinen Ortes mit Name
		// Falls der Standard Name überschrieben werden soll
		public Ort ( string in_name )
		{
			name = in_name;
		}

		// -- Verknüpfung von Orten
		// Parameter sind - das Schlüsselwort, das weiter führt
		// - eine Instanz des Ortes, wohin die Verknüpfung führt
		public void VerknuepfeOrt( string in_schluessel, Ort in_neuer_ort )
		{
			//verknuepfungen.Add( in_schluessel, in_neuer_ort );
			verknuepfungen[ in_schluessel ] = in_neuer_ort;
		}

		// -- Starten eines Ortes
		public void LosGehts()
		{
			AusfuehrlicheBeschreibungAusgeben ();
			WegbeschreibungAusgeben ();

			string kommando;
			do {
				// Was will der Spieler tun?
				Console.WriteLine( "What do you want to do?" );
				kommando = Console.ReadLine ();

				// Prüfe ob das kommando als Bewegung erkannt wird
				if ( verknuepfungen.ContainsKey (kommando) ) {
					// Juchu, es ist ein Richtungskommando
					// Hole den Ort, zu dem dieses Kommando führt
					Ort neuer_ort = verknuepfungen [kommando];
					// Ort neuer_ort = verknuepfungen.VGet( kommando );

					Console.WriteLine();  // Leerzeile ausgeben
					neuer_ort.LosGehts ();	// neuen Ort ausführen

				} else if ( IstCustomCommand( kommando ) ) {
					// Juchu, es ist ein Custom Command
					BehandleCustomCommand( kommando );

				} else if ( IstDingKommando( kommando ) ) {
					// Juchu, es ist ein Kommando mit Ding
					BehandleDingKommando( kommando );

				} else if ( kommando == "things" ) {
					// Es ist ein allgemeines Kommando
					DingListeAusgeben();

				} else {
					Console.WriteLine ("Hmmm... that seems to be impossible around here.");
					Console.WriteLine();
				}
			} while ( !verknuepfungen.ContainsKey (kommando) );
		}

		// Methode gibt die ausführliche Beschreibung
		// eines Ortes aus
		public void AusfuehrlicheBeschreibungAusgeben() {
			// Textausgabe des Ortes / Beschrieb
			Console.WriteLine ("You are " + bezug + " the " + name);
			// Beschrieb ausgeben, haben wir noch nicht
			Console.WriteLine (beschrieb);
		}

		// Methode gibt die Liste der Richtungs-
		// kommandos und die damit erreichbaren Räume
		// aus
		//                                          -|
		public void WegbeschreibungAusgeben() {
			// Danach gangbare Wege anzeigen
			// Vorgehensweise:
			// Schlaufe durch alle Einträge in verknuepfungen
			// Ausgabe
			// - richtung, in die man gehen kann
			// - name des Ortes, der dann erreicht wird
			foreach (var verknuepfung in verknuepfungen) {
				string richtung = verknuepfung.Key;
				Ort ort = verknuepfung.Value;
				string name = ort.name;

				Console.WriteLine ("To the " + richtung + " you see " + name);
			}
		}

		// Methode gibt die Liste der Dinge aus, die
		// sich im Ort befinden
		//                                          -|
		public void DingListeAusgeben() {
			Console.Write( "You can see the following things around here: " );

			// Liste der Einträge in dinge ausgeben
			// Schlaufe durch alle Einträge in dinge
			// Ausgabe
			// - name des Dings
			// - Komma, wenn nicht das letzte
			foreach ( var ding_eintrag in dinge ) {
				string name = ding_eintrag.Key;

				if ( !ding_eintrag.Equals( dinge.Last() ) ) {
					Console.Write( name + ", " );
				} else {
					Console.WriteLine( name );
				}
			}
		}


		// Abstrakte Formulierung einer Methode, die
		// Custom Commands identifiziert
		// Wird hier nicht implementiert, sondern dient
		// als Vorgabe, die die erbenden, speziellen Räume
		// implementieren müssen
		public abstract bool IstCustomCommand (string in_kommando);

		// Abstrakte Formulierung einer Methode, die
		// Custom Commands behandelt
		// Wird hier nicht implementiert, sondern dient
		// als Vorgabe, die die erbenden, speziellen Räume
		// implementieren müssen
		public abstract void BehandleCustomCommand (string in_kommando);

		public void VerknuepfeDing ( Ding in_ding )
		{
			// Speichere das Ding unter seinem Namen im
			// Dictionary für dinge
			dinge[ in_ding.name ] = in_ding;
		}

		public bool IstDingKommando( string in_kommando ) {

			// Teile das Kommando auf
			// Teile an der Leertaste auf
			// Resultat zwei einzelne Strings (können auch mehr sein)
			// Nimm den zweiten Teil (... dritten, vierten)

			// Schlaufe durchs Dictionary dinge durch
			// Namen aus dem Eintrag holen (Key)
			// Vergleiche: Zweiter Teil in_kommando
			//  mit Namen (Key von Eintrag)
			// wenns passt
			// Ding aus Eintrag holen ( Value)
			return false;
		}

		public void BehandleDingKommando( string in_kommando ) {
		}
	}
}
