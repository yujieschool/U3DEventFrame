using UnityEngine;

using System.Collections;

using  U3DEventFrame ;

//
//  UIEvents#FILEEXTENSION#
//  #PROJECTNAME#
//
//  Created by #SMARTDEVELOPERS# on #CREATIONDATE#.
//
//


public enum UIPanel_Ma
{
 
     Initial = ManagerID.UIManager +1,

     Loading ,
     Regist ,

      MaxValue
}

public enum UIPanel_Zhou
{
    Initial = UIPanel_Ma.MaxValue,

     ButtonClick,

     ChangeName,

     MaxValue

}