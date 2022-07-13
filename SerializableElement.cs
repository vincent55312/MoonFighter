using System;
using System.Collections.Generic;
using System.Text;

interface SerializableElement
{
    public void save();
    public object getFromSave();
}
