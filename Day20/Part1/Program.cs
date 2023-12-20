// Day 20
// Part 1

string[] lines = File.ReadAllLines("Part1/input.txt");

Dictionary<string, Module> modules = new Dictionary<string, Module>();
Dictionary<string, bool> flipFlops = new Dictionary<string, bool>();
Dictionary<string, Dictionary<string, string>> conjunctions = new Dictionary<string, Dictionary<string, string>>();

Queue<Pulse> pulses = new Queue<Pulse>();

foreach (string line in lines)
{
    if (line.Contains("broadcaster"))
    {
        string[] broadcasts = line.Split(" -> ")[1].Split(", ");
        modules.Add("broadcaster", new Module(' ', "broadcaster", broadcasts));
    }
    else
    {
        char type = line[0];
        string[] split = line.Split(" -> ");

        string mod = split[0][1..];
        string[] dest = split[1].Split(", ");
        modules.Add(mod, new Module(type, mod, dest));
        if (type == '%')
        {
            flipFlops.Add(mod, false);
        }
        else if (type == '&')
        {
            conjunctions.Add(mod, new Dictionary<string, string>());
        }
    }
}

// Set conjunctions to low by default
foreach (Module module in modules.Values)
{
    foreach (string dest in module.dest)
    {
        if (!modules.ContainsKey(dest))
        {
            continue;
        }

        Module destModule = modules[dest];
        if (destModule.type == '&')
        {
            conjunctions[destModule.mod].Add(module.mod, "lo");
        }
    }
}

int low = 0;
int high = 0;
void SendPulse(string pulse, string from, string[] dest)
{
    foreach (string mod in dest)
    {
        if (pulse == "lo")
        {
            low++;
        }
        else
        {
            high++;
        }

        //Console.WriteLine(from + " -" + pulse + "-> " + mod);

        if (!modules.ContainsKey(mod))
        {
            continue;
        }

        pulses.Enqueue(new Pulse(pulse, from, mod));
    }
}

void PressButton()
{
    low++; // Button pulse
    SendPulse("lo", "broadcaster", modules["broadcaster"].dest);

    while (pulses.TryDequeue(out Pulse? p))
    {
        string pulse = p.pulse;
        Module fromMod = modules[p.from];
        Module module = modules[p.to];

        if (module.type == '%')
        {
            if (pulse == "lo")
            {
                bool ff = flipFlops[module.mod];
                if (!ff)
                {
                    flipFlops[module.mod] = true;
                    SendPulse("hi", module.mod, module.dest);
                }
                else
                {
                    flipFlops[module.mod] = false;
                    SendPulse("lo", module.mod, module.dest);
                }
            }
        }
        else if (module.type == '&')
        {
            conjunctions[module.mod][fromMod.mod] = pulse;

            bool allHigh = true;
            foreach (string memPulse in conjunctions[module.mod].Values)
            {
                if (memPulse == "lo")
                {
                    allHigh = false;
                    break;
                }
            }

            if (allHigh)
            {
                SendPulse("lo", module.mod, module.dest);
            }
            else
            {
                SendPulse("hi", module.mod, module.dest);
            }
        }
    }
}

for(int i = 0; i < 1000; i++)
{
    PressButton();
}
Console.WriteLine("Low: " + low + " High: " + high);
Console.WriteLine(low * high);

record Pulse(string pulse, string from, string to);

record Module(char type, string mod, string[] dest);