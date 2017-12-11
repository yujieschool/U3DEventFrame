

LTestC= {}

LTestC.__index = LTestC



function LTestC.FindObj( ... )
	-- body

   tmpGameObj=  GameObject.Find("Cube")

   tmpGameObj.name= "Test"

end

function LTestC.ChangeName( tmpObj )
	-- body

 

   tmpObj.name= "TestOne"

end



LTestC.FindObj()


