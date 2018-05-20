using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCommandHandler : ScenarioCommandHandler
{
    public class Options : CommandOptions
    {
        public ItemEntity ItemEntity { get; private set; }
        public bool Flg { get; private set; }

        Options(ItemEntity itemEntity, bool flg)
        {
            ItemEntity = itemEntity;
            Flg = flg;
        }

        public static Options Create(Scenario scenario)
        {
            var itemEntity = ItemEntity.FindByItemName(scenario.Arg1);
            if (itemEntity == null)
            {
                Debug.LogWarningFormat("Item Error: [{0}] not found", scenario.Arg1);
            }
            bool flg = System.Convert.ToBoolean(scenario.Arg2);
            return new Options(itemEntity, flg);
        }
    }

    public ItemCommandHandler(ScenarioManager scenarioManager) : base(scenarioManager)
    {
    }

    public override void OnCommand(CommandOptions commandOptions)
    {
        base.OnCommand(commandOptions);
        OnCommandBg(commandOptions as Options);
    }

    void OnCommandBg(Options options)
    {
        options.ItemEntity.UserItem.ToggleIsOwned(options.Flg);
        GameDataManager.Instance.SaveData();
    }
}
