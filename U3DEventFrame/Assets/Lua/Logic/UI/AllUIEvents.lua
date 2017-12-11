

local LUIEventBegin  = LManagerID.LUIManager +1


LUIBagEvent = {

   "Initial",
    "Back",
    "MaxValue",

    Initial,
    Back,
    MaxValue
}

ResetTableKeyValue(LUIEventBegin ,LUIBagEvent);


require "Logic.UI.Events.UI_ZhangSanEvents"

require "Logic.UI.Events.UI_LiSiEvents"

