﻿namespace Logic.Generators.Generated
{
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Xml.Serialization;

// 
// This source code was auto-generated by xsd, Version=4.8.3928.0.
// 


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://tempuri.org/Xsd/HubEndpoint.xsd")]
[System.Xml.Serialization.XmlRootAttribute(Namespace="http://tempuri.org/Xsd/HubEndpoint.xsd", IsNullable=false)]
public partial class HubEndpoint {
    
    private string urlField;
    
    private ReferenceType[] referencesField;
    
    private EndpointType clientField;
    
    private EndpointType serverField;
    
    /// <remarks/>
    public string Url {
        get {
            return this.urlField;
        }
        set {
            this.urlField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Reference", IsNullable=false)]
    public ReferenceType[] References {
        get {
            return this.referencesField;
        }
        set {
            this.referencesField = value;
        }
    }
    
    /// <remarks/>
    public EndpointType Client {
        get {
            return this.clientField;
        }
        set {
            this.clientField = value;
        }
    }
    
    /// <remarks/>
    public EndpointType Server {
        get {
            return this.serverField;
        }
        set {
            this.serverField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/Xsd/HubEndpoint.xsd")]
public partial class ReferenceType {
    
    private string namespaceField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Namespace {
        get {
            return this.namespaceField;
        }
        set {
            this.namespaceField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/Xsd/HubEndpoint.xsd")]
public partial class ParameterType {
    
    private string nameField;
    
    private string typeField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Name {
        get {
            return this.nameField;
        }
        set {
            this.nameField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Type {
        get {
            return this.typeField;
        }
        set {
            this.typeField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/Xsd/HubEndpoint.xsd")]
public partial class MethodType {
    
    private string nameField;
    
    private ParameterType[] parametersField;
    
    /// <remarks/>
    public string Name {
        get {
            return this.nameField;
        }
        set {
            this.nameField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Parameter", IsNullable=false)]
    public ParameterType[] Parameters {
        get {
            return this.parametersField;
        }
        set {
            this.parametersField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/Xsd/HubEndpoint.xsd")]
public partial class EndpointType {
    
    private MethodType[] methodsField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Method", IsNullable=false)]
    public MethodType[] Methods {
        get {
            return this.methodsField;
        }
        set {
            this.methodsField = value;
        }
    }
}
}
