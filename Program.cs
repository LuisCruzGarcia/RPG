using System;

namespace RPG
{
    class Program
    {
        static public int boss_defeated = 0;
        static int inventarioId = 0; 
        static Objeto[] inventario = new Objeto[5];

        static void Main(string[] args)
        {
            // string name, int fuerza, int defensa, int agilidad, int dinero, int hit_points
            Player jugador = new Player("Luis", 30, 15, 60, 100, 50);

            Objeto pocion_de_Destresa = new Objeto("Poción piernas rápidas", 10, "agilidad");
            Objeto espada_Magica = new Objeto("Excalibur", 10, "fuerza");
            Objeto pocion_de_Vida = new Objeto("Poción de las Hadas", 15, "vida");
            Objeto escudo_Magico = new Objeto("Escudo Hyliano", 10, "defensa");
            Objeto estrella = new Objeto("Estrella final", 5, "estadisticas");

            // string enemy_name, int fuerza, int defensa, int agilidad, int dinero, int hit_points, objeto
            Enemy giant_frog = new Enemy("Demon of song", 20, 5, 20, 15, 10, "El Sapo demonio", pocion_de_Destresa);
            Enemy spider = new Enemy("Ella", 20, 10, 30, 20, 15, "La araña", espada_Magica);
            Enemy hada = new Enemy("Carabose", 25, 5, 50, 25, 15, "La hada", pocion_de_Vida);
            Enemy troll = new Enemy("Ugma", 40, 15, 5, 30, 45, "El troll", escudo_Magico);
            Enemy nito = new Enemy("Nito", 50, 25, 35, 30, 55, "El rey del inframundo", estrella);

            int enemigosRestantes = boss_defeated + 4;

            while (boss_defeated < 4)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;

                Console.WriteLine("\nEnfrente de ti se encuentran " + enemigosRestantes + " puertas, cada una con un desafío diferente. " +
                    "Vence aquellos que guardan los 4 items legendarios, una vez reunidos los objetos seras invencible. A que puerta entraras.");

                int puerta = Convert.ToInt32(Console.ReadLine());
                Console.ResetColor();

                switch (puerta)
                {
                    case 1:
                        Console.WriteLine("\nUna vez pasada la primera puerta no hay marcha atras. En tus pies sientes agua fria y salada. Frente a ti hay una criatura morbosa y babosa.");
                        Console.ResetColor();
                        fightEngine(jugador, giant_frog);
                        enemigosRestantes--;
                        break;
                    case 2:
                        Console.WriteLine("\nEn la segunda sala no puedes ver nada más que una brisa. Tocas la oscuridad y tu mano se pega a la pared. De pronto es dificil moverse.\n" +
                            "En la oscuridad solo puedes ver 8 ojos luminosos. Ella la araña esta lista.");
                        Console.ResetColor();
                        fightEngine(jugador, spider);
                        enemigosRestantes--;

                        break;
                    case 3:
                        Console.WriteLine("\nTras la tercera puerta vez un campo moribundo con un árbol apagado en el centro. Escuchas una risa particular. Un zumbido pasa por tu oreja.\n" +
                            "Pelear contra tu siguiente enemigo será complicado. Ya que es más pequeño que tu mano.");
                        Console.ResetColor();
                        fightEngine(jugador, hada);
                        enemigosRestantes--;

                        break;
                    case 4:
                        Console.WriteLine("\nLa última puerta de la derecha. La cuarta puerta. Vez un solo puente entre dos claros. En medio vez a una criatura de 5 palmos de altura.");
                        Console.ResetColor();
                        fightEngine(jugador, troll);
                        enemigosRestantes--;
                        break;
                    default:
                        Console.WriteLine("\nIngresa un número correcto\n");
                        Console.ResetColor();

                        break;
                }
            }

            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine("Una vez terminada la última puerta puedes ver a un ángel. No. Es un demonio. Nito hace su aparición. Tu último desafío, heróe de las eras.");
            Console.ResetColor();

            fightEngine(jugador, nito);

            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine("Has pasado la prueba. Heroé de las eras!");
            Console.ResetColor();
        }

        static void fightEngine(Player player, Enemy enemy)
        {
            Console.WriteLine("\nEstas luchando contra " + enemy.getEnemyName() + " " + enemy.getTipo() + "\nNo pierdas\n");

            Random rnd = new Random();
            bool enemyDeath = false;

            while (!enemyDeath)
            {
                Console.WriteLine("\n1 ataque / 2 revisar tus estadísticas / 3 revisar inventario\n");
                int input = Convert.ToInt32(Console.ReadLine());

                if (input == 1){
                    int chance_attack = rnd.Next(1, 101);
                    if (chance_attack > enemy.getAgilidad()) {

                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.DarkGreen;

                        int damage = player.getDamage() - enemy.getDefensa();
                        Console.WriteLine("\nAtacaste a " + enemy.getEnemyName() + ". El enemigo toma " + damage + " puntos de daño");
                        enemy.takeDamage(damage);
                        Console.WriteLine("Enemigo HP: " + enemy.getHP());

                        if (enemy.getHP() > 0)
                        {
                            Console.WriteLine("El enemigo sigue de pie");
                        }

                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;

                        Console.WriteLine("Missed!");

                        Console.ResetColor();
                    }

                    int chance_receive_attack = rnd.Next(1, 101);

                    if (chance_receive_attack > player.getAgilidad()){

                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.DarkRed;

                        int damage = enemy.getDamage() - player.getDefensa();
                        Console.WriteLine("\nEl enemigo ha contraatacado, tomas " + damage + " puntos de daño");
                        player.takeDamage(damage);
                        Console.WriteLine("Tu HP: " + player.getHP());

                        if (player.getHP() <= 0){
                            Console.WriteLine("Game Over");
                            Environment.Exit(1);
                        }

                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;

                        Console.WriteLine("Esquivaste el ataque");

                        Console.ResetColor();
                    }

                    if (enemy.getHP() <= 0) {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.DarkYellow;

                        enemy.deleteEnemy();

                        Console.WriteLine("\nEl enemigo ha soltado un objeto: " + enemy.getObjeto().getNombre());
                        Console.WriteLine("Este objeto aumenta tu " + enemy.getObjeto().getEfecto() + " en " + enemy.getObjeto().getSumatorio() + " puntos.");
                        Console.WriteLine("Recibes " + enemy.getDinero() + " de dinero.\n");

                        player.takeMoney(enemy.getDinero());
                        player.equipItem(enemy.getObjeto().getSumatorio(), enemy.getObjeto().getEfecto());
                        inventario.SetValue(enemy.getObjeto(), inventarioId);
                        inventarioId++;
                        boss_defeated++;
                        enemyDeath = true;

                        Console.ResetColor();
                    }
                }
                else if(input == 2){
                    player.getStatus();
                } else if(input == 3)
                {
                    getInventario();
                }
            }
        }

        static void getInventario()
        {
            for(int i = 0; i <= inventario.Length; i++)
            {
                Console.WriteLine("\nInventario: \n");
                if (inventario[i] != null) {
                    Console.WriteLine(inventario[i].getNombre() +  " aumenta tu " + inventario[i].getEfecto()  + " en " + inventario[i].getSumatorio());
                }
                else
                {
                    break;
                }
            }
        }
    }

    class Player
    {
        string name;
        int fuerza;
        int defensa;
        int agilidad;
        int dinero;
        int hit_points;

        public Player(string name, int fuerza, int defensa, int agilidad, int dinero, int hit_points)
        {
            this.name = name;
            this.fuerza = fuerza;
            this.defensa = defensa;
            this.agilidad = agilidad;
            this.dinero = dinero;
            this.hit_points = hit_points;
        }

        public void equipItem(int sumatorio, string efecto)
        {
            switch (efecto)
            {
                case "fuerza":
                    fuerza += sumatorio;
                    break;
                case "defensa":
                    defensa += sumatorio;
                    break;
                case "agilidad":
                    agilidad += sumatorio;
                    break;
                case "vida":
                    hit_points += sumatorio;
                    break;
                case "estadisticas":
                    hit_points += sumatorio;
                    agilidad += sumatorio;
                    defensa += sumatorio;
                    fuerza += sumatorio;
                    break;
                default:
                    Console.WriteLine("Default");
                    break;
            }
        }

        public int getDefensa()
        {
            return defensa;
        }
        public int getAgilidad()
        {
            return agilidad;
        }

        public int getDamage()
        {
            return fuerza;
        }

        public int getHP()
        {
            return hit_points;
        }

        public void takeDamage(int damage_taken)
        {
            hit_points -= damage_taken;
        }

        public void takeMoney(int money)
        {
            dinero += money;
        }

        public void getStatus()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkCyan;

            Console.WriteLine($"{name} HP: {hit_points}\n\nFuerza: {fuerza} \nDefensa: {defensa} \nAgilidad: {agilidad} \n\nOro: {dinero}");

            Console.ResetColor();
        }
    }

    class Objeto
    {
        string nombre;
        int sumatorio;
        string efecto;

        public Objeto(string nombre, int sumatorio, string efecto)
        {
            this.nombre = nombre;
            this.sumatorio = sumatorio;
            this.efecto = efecto;
        }

        public string getNombre()
        {
            return nombre;
        }

        public int getSumatorio()
        {
            return sumatorio;
        }

        public string getEfecto()
        {
            return efecto;
        }
    }

    class Enemy
    {
        string enemy_name;
        int hit_points;
        int fuerza;
        int defensa;
        int agilidad;
        int dinero;
        string tipo_enemigo;
        Objeto objeto;

        public Enemy(string enemy_name,  int fuerza, int defensa, int agilidad, int dinero, int hit_points, string tipo_enemigo, Objeto objeto)
        {
            this.enemy_name = enemy_name;
            this.hit_points = hit_points;
            this.fuerza = fuerza;
            this.defensa = defensa;
            this.agilidad = agilidad;
            this.dinero = dinero;
            this.objeto = objeto;
            this.tipo_enemigo = tipo_enemigo;
        }

        public string getEnemyName()
        {
            return enemy_name;
        }

        public int getDamage()
        {
            return fuerza;
        }

        public Objeto getObjeto()
        {
            return objeto;
        }

        public int getDefensa()
        {
            return defensa;
        }

        public int getAgilidad()
        {
            return agilidad;
        }

        public int getDinero()
        {
            return dinero;
        }

        public int getHP()
        {
            return hit_points;
        }

        public string getTipo()
        {
            return tipo_enemigo;
        }

        public void takeDamage(int damage_taken)
        {
            hit_points -= damage_taken;
        }

        public void deleteEnemy()
        {
            if(hit_points <= 0)
            {
                Console.WriteLine("El enemigo " + this.enemy_name + " ha muerto.");
            }
            else
            {
                Console.WriteLine("El enemigo aun esta de pie");
            }
        }
    }
}