using System;
using TextAdventure;

namespace TextAdventure
{
	// Interface
	// Jedes Ding muss einen Namen besitzen
	interface IDing
	{
		string name
		{
			get;
			set;
		}
	}
	
	public class Ding : IDing
	{
		// -- Properties, die alle Dinge haben sollen
		// Der Name eines Dings
		// Die Variable ist privat und kann nur ueber die vom Interface definierte Eigenschaft
		// "name" gelesen werden
		private string privateName = "untitled item";

		public Ding ( string in_name )
		{
			privateName = in_name;
		}
		
		public string name
		{
			get { return privateName; }
			set { privateName = value; }
		}
	}
}
