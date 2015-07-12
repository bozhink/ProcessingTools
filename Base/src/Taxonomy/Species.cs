using System;
using System.Text.RegularExpressions;

namespace Base
{
	namespace Taxonomy
	{
		public class SpeciesComparison
		{
			private bool _genus;
			private bool _subgenus;
			private bool _species;
			private bool _subspecies;
			public bool genus { get { return _genus; } }
			public bool subgenus { get { return _subgenus; } }
			public bool species { get { return _species; } }
			public bool subspecies { get { return _subspecies; } }

			public SpeciesComparison()
			{
				_genus = false;
				_subgenus = false;
				_species = false;
				_subspecies = false;
			}

			public SpeciesComparison(Species sp1, Species sp2)
			{
				_genus = String.Compare(sp1.genus, sp2.genus) == 0;
				_subgenus = String.Compare(sp1.subgenus, sp2.subgenus) == 0;
				_species = String.Compare(sp1.species, sp2.species) == 0;
				_subspecies = String.Compare(sp1.subspecies, sp2.subspecies) == 0;
			}
		}

		public class Species
		{
			public static Regex GenusName = new Regex("(?<=type=\"genus\"[^>]*>)[A-Z][a-z\\.]+(?=</t)");
			public static Regex SubgenusName = new Regex("(?<=type=\"subgenus\"[^>]*>)[A-Z][a-z\\.]+(?=</t)");
			public static Regex SpeciesName = new Regex("(?<=type=\"species\"[^>]*>)[a-z\\-\\.]+(?=</t)");
			public static Regex SubspeciesName = new Regex("(?<=type=\"subspecies\"[^>]*>)[a-z\\-]+(?=</t)");

			protected string _genus;
			protected string _subgenus;
			protected string _species;
			protected string _subspecies;
			protected bool _isGenusNull;
			protected bool _isSubgenusNull;
			protected bool _isSpeciesNull;
			protected bool _isSubspeciesNull;
			protected bool _isShortened;

			public string genus { get { return _genus; } }
			public string subgenus { get { return _subgenus; } }
			public string species { get { return _species; } }
			public string subspecies { get { return _subspecies; } }
			public bool isGenusNull { get { return _isGenusNull; } }
			public bool isSubgenusNull { get { return _isSubgenusNull; } }
			public bool isSpeciesNull { get { return _isSpeciesNull; } }
			public bool isSubspeciesNull { get { return _isSubspeciesNull; } }
			public bool isShortened { get { return _isShortened; } }

			public string genusTagged { get { return "<tn-part type=\"genus\">" + _genus + "</tn-part>"; } }
			public string subgenusTagged { get { return "<tn-part part-type=\"subgenus\">" + _subgenus + "</tn-part>"; } }
			public string speciesTagged { get { return "<tn-part type=\"species\">" + _species + "</tn-part>"; } }
			public string subspeciesTaged { get { return "<tn-part type=\"subspecies\">" + _subspecies + "</tn-part>"; } }

			public string genusPattern
			{
				get
				{
					return (_genus.IndexOf('.') > -1) ? "\\b" + _genus.Substring(0, _genus.IndexOf('.')) + "[a-z\\-]+?" : "\\b" + _genus + "\\b";
				}
			}
			public string subgenusPattern
			{
				get
				{
					return (_subgenus.IndexOf('.') > -1) ? "\\b" + _subgenus.Substring(0, _subgenus.IndexOf('.')) + "[a-z\\-]+?" : "\\b" + _subgenus + "\\b";
				}
			}
			public string speciesPattern
			{
				get
				{
					return (_species.IndexOf('.') > -1) ? "\\b" + _species.Substring(0, _species.IndexOf('.')) + "[a-z\\-]+?" : "\\b" + _species + "\\b";
				}
			}
			public string subspeciesPattern
			{
				get
				{
					return (_subspecies.IndexOf('.') > -1) ? "\\b" + _subspecies.Substring(0, _subspecies.IndexOf('.')) + "[a-z\\-]+?" : "\\b" + _subspecies + "\\b";
				}
			}

			public string genusSkipPattern
			{
				get
				{
					return (_genus.IndexOf('.') > -1) ? "\\b" + _genus.Substring(0, _genus.IndexOf('.')) + "[a-z\\-]+?" : "SKIP";
				}
			}
			public string subgenusSkipPattern
			{
				get
				{
					return (_subgenus.IndexOf('.') > -1) ? "\\b" + _subgenus.Substring(0, _subgenus.IndexOf('.')) + "[a-z\\-]+?" : "SKIP";
				}
			}
			public string speciesSkipPattern
			{
				get
				{
					return (_species.IndexOf('.') > -1) ? "\\b" + _species.Substring(0, _species.IndexOf('.')) + "[a-z\\-]+?" : "SKIP";
				}
			}
			public string subspeciesSkipPattern
			{
				get
				{
					return (_subspecies.IndexOf('.') > -1) ? "\\b" + _subspecies.Substring(0, _subspecies.IndexOf('.')) + "[a-z\\-]+?" : "SKIP";
				}
			}

			public string speciesName
			{
				get
				{
					string name = _isGenusNull ? string.Empty : _genus;
					name += _isSubgenusNull ? string.Empty : " (" + _subgenus + ")";
					name += _isSpeciesNull ? string.Empty : " " + _species;
					name += _isSubspeciesNull ? string.Empty : " " + _subspecies;
					return name;
				}
			}

			public string speciesNameGenusSubgenus
			{
				get
				{
					string name = _isGenusNull ? string.Empty : _genus;
					name += _isSubgenusNull ? string.Empty : " (" + _subgenus + ")";
					name += _isSpeciesNull ? string.Empty : " [" + _species + "]";
					name += _isSubspeciesNull ? string.Empty : " [" + _subspecies + "]";
					return name;
				}
			}

			public Species(string parsedContent)
			{
				Match m = Species.GenusName.Match(parsedContent);
				_genus = m.Success ? m.Value : string.Empty;
				m = Species.SubgenusName.Match(parsedContent);
				_subgenus = m.Success ? m.Value : string.Empty;
				m = Species.SpeciesName.Match(parsedContent);
				_species = m.Success ? m.Value : string.Empty;
				m = Species.SubspeciesName.Match(parsedContent);
				_subspecies = m.Success ? m.Value : string.Empty;
				_isGenusNull = IsGenusNull();
				_isSubgenusNull = IsSubgenusNull();
				_isSpeciesNull = IsSpeciesNull();
				_isSubspeciesNull = IsSubspeciesNull();
				_isShortened = IsShortened();
			}

			public Species(string genus, string subgenus, string species, string subspecies)
			{
				this._genus = genus;
				this._subgenus = subgenus;
				this._species = species;
				this._subspecies = subspecies;
				_isGenusNull = IsGenusNull();
				_isSubgenusNull = IsSubgenusNull();
				_isSpeciesNull = IsSpeciesNull();
				_isSubspeciesNull = IsSubspeciesNull();
				_isShortened = IsShortened();
			}

			public Species()
			{
				_genus = string.Empty;
				_subgenus = string.Empty;
				_species = string.Empty;
				_subspecies = string.Empty;
				_isGenusNull = true;
				_isSubgenusNull = true;
				_isSpeciesNull = true;
				_isSubspeciesNull = true;
				_isShortened = false;
			}

			public Species(Species sp)
			{
				this._genus = sp.genus;
				this._subgenus = sp.subgenus;
				this._species = sp.species;
				this._subspecies = sp.subspecies;
				_isGenusNull = IsGenusNull();
				_isSubgenusNull = IsSubgenusNull();
				_isSpeciesNull = IsSpeciesNull();
				_isSubspeciesNull = IsSubspeciesNull();
				_isShortened = IsShortened();
			}

			public void SetGenus(string genus)
			{
				this._genus = genus;
				_isGenusNull = IsGenusNull();
				_isShortened = IsShortened();
			}

			public void SetSubgenus(string subgenus)
			{
				this._subgenus = subgenus;
				_isSubgenusNull = IsSubgenusNull();
				_isShortened = IsShortened();
			}

			public void SetSpecies(string species)
			{
				this._species = species;
				_isSpeciesNull = IsSpeciesNull();
				_isShortened = IsShortened();
			}

			public void SetSubspecies(string subspecies)
			{
				this._subspecies = subspecies;
				_isSubspeciesNull = IsSubspeciesNull();
			}

			public void SetGenus(Species sp)
			{
				this._genus = sp.genus;
				_isGenusNull = sp.isGenusNull;
				_isShortened = IsShortened();
			}

			public void SetSubgenus(Species sp)
			{
				this._subgenus = sp.subgenus;
				_isSubgenusNull = sp.isSubgenusNull;
				_isShortened = IsShortened();
			}

			public void SetSpecies(Species sp)
			{
				this._species = sp.species;
				_isSpeciesNull = sp.isSpeciesNull;
				_isShortened = IsShortened();
			}

			public void SetSubspecies(Species sp)
			{
				this._subspecies = sp.subspecies;
				_isSubspeciesNull = sp.isSubspeciesNull;
			}

			private bool IsGenusNull()
			{
				return (_genus.Length == 0);
			}

			private bool IsSubgenusNull()
			{
				return (_subgenus.Length == 0);
			}

			private bool IsSpeciesNull()
			{
				return (_species.Length == 0);
			}

			private bool IsSubspeciesNull()
			{
				return (_subspecies.Length == 0);
			}

			private bool IsShortened()
			{
				string sp = _genus + _subgenus + _species + _subspecies;
				return (sp.IndexOf('.') > -1);
			}

			public string AsString()
			{
				string result = string.Empty;
				if (!_isGenusNull) result += _genus;
				if (!_isSubgenusNull)
				{
					if (result.Length != 0) result += " ";
					result += "(" + _subgenus + ")";
				}
				if (!_isSpeciesNull)
				{
					if (result.Length != 0) result += " ";
					result += _species;
				}
				if (!_isSubgenusNull)
				{
					if (result.Length != 0) result += " ";
					result += _subspecies;
				}
				return result;
			}
		}
	}
}
