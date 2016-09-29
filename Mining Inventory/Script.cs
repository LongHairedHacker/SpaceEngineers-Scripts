
public void Main() {
    List<string> names = new List<string>() 	{
        "Uranium Ingot",
        "Uranium Ore",
        "Iron Ore",
        "Iron Ingot",
        "Nickel Ore",
        "Nickel Ingot",
        "Magnesium Ore",
        "Magnesium Powder",
        "Gold Ore",
        "Gold Ingot",
        "Silver Ore",
        "Silver Ingot",
        "Platinum Ore",
        "Platinum Ingot",
        "Silicon Ore",
        "Silicon Wafer",
        "Ice"
    };


    var display = GridTerminalSystem.GetBlockWithName("OreDisplay") as  IMyTextPanel;

    var blocks = new List<IMyInventoryOwner>();
    GridTerminalSystem.GetBlocksOfType<IMyInventoryOwner>(blocks);

    display.WritePublicText("Found " + blocks.Count + " Inventories");
    display.ShowTextureOnScreen();
    display.ShowPublicTextOnScreen();

    var amounts = new Dictionary<String, double >();
    foreach(var name in names) {
        amounts.Add(name, 0.0);
    }

    var text = "Inventory:\n";

    foreach(var block in blocks) {
        for(var i = 0; i < block.InventoryCount; i++) {
            var items = block.GetInventory(i).GetItems();
            foreach(var item in items) {
                var itemName = item.Content.SubtypeName;

                if(item.Content.TypeId.ToString().Contains("_Ore")) {
                    if(!itemName.Contains("Ice")) {
                        itemName += " Ore";
                    }
                }
                else if(item.Content.TypeId.ToString().Contains("_Ingot")) {
                    if(itemName.Contains("Magnesium")) {
                        itemName += " Powder";
                    }
                    else if(itemName.Contains("Silicon")) {
                        itemName += " Wafer";
                    }
                    else {
                        itemName += " Ingot";
                    }
                }

                if(amounts.ContainsKey(itemName)) {
                    var amountKilo = ((double) item.Amount.RawValue) / 1000000;
                    amounts[itemName] += amountKilo;
                }
            }
        }
    }


    foreach(var pair in amounts) {
        if(pair.Value <= 1000) {
            text += pair.Key + ": " + pair.Value.ToString("N3") + " kg\n";
        }
        else {
            text += pair.Key + ": " + (pair.Value / 1000).ToString("N3") + " t\n";
        }
    }

    display.WritePublicText(text);
    display.ShowTextureOnScreen();
    display.ShowPublicTextOnScreen();
}
