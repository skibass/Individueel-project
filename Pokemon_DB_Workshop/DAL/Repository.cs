using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Xml.Linq;

namespace DAL
{
    public class Repository
    {
        MySqlConnection db = new MySqlConnection("Server=127.0.0.1;Database=pokemon;Uid=root;Pwd=;");

        List<Pokemon> pokemonList = new List<Pokemon>();

        public List<Pokemon> GetPokemon()
        {
            pokemonList.Clear();

            db.Open();

            MySqlCommand pokemonQuery = new MySqlCommand("SELECT * from pokemon", db);

            pokemonQuery.ExecuteNonQuery();

            MySqlDataReader readPokemons = pokemonQuery.ExecuteReader();

            while (readPokemons.Read())
            {
                Pokemon poke = new Pokemon
                {
                    Name = ""
                };

                poke.Name = (string)readPokemons["name"];
                poke.MaxHp = (int)readPokemons["max_hp"];
                poke.MinHp = (int)readPokemons["min_hp"];

                pokemonList.Add(poke);
            }
            db.Close();

            return pokemonList;
        }

        public void AddPokemon(Pokemon pokemon)
        {
            db.Open();

            MySqlCommand cmd = new MySqlCommand($"INSERT INTO pokemon (name, max_hp, min_hp) VALUES (@Name, @MaxHp,@MinHp)", db);

            cmd.Parameters.Add(new MySqlParameter("@Name", MySqlDbType.VarChar));
            cmd.Parameters.Add(new MySqlParameter("@MaxHp", MySqlDbType.VarChar));
            cmd.Parameters.Add(new MySqlParameter("@MinHp", MySqlDbType.VarChar));

            cmd.Parameters["@Name"].Value = pokemon.Name;
            cmd.Parameters["@MaxHp"].Value = pokemon.MaxHp;
            cmd.Parameters["@MinHp"].Value = pokemon.MinHp;

            cmd.ExecuteNonQuery();

            pokemonList.Add(pokemon);

            db.Close();
        }
    }
}
