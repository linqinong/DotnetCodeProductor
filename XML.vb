Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Security

Imports System.Xml


''' <summary>
''' XmlHelper 的摘要说明
''' </summary>
Public Class XmlHelper
    Public Sub New()
    End Sub

    ''' <summary>
    ''' 读取数据
    ''' </summary>
    ''' <param name="path">路径</param>
    ''' <param name="node">节点</param>
    ''' <param name="attribute">属性名，非空时返回该属性值，否则返回串联值</param>
    ''' <returns>string</returns>
    '*************************************************
    '         * 使用示列:
    '         * XmlHelper.Read(path, "/Node", "")
    '         * XmlHelper.Read(path, "/Node/Element[@Attribute='Name']", "Attribute")
    '         ***********************************************

    Public Shared Function Read(ByVal path As String, ByVal node As String, ByVal attribute As String) As String
        Dim value As String = ""
        Try
            Dim doc As New XmlDocument()
            doc.Load(path)
            Dim xn As XmlNode = doc.SelectSingleNode(node)
            value = (If(attribute.Equals(""), xn.InnerText, xn.Attributes(attribute).Value))
        Catch
        End Try
        Return value
    End Function

    ''' <summary>
    ''' 插入数据
    ''' </summary>
    ''' <param name="path">路径</param>
    ''' <param name="node">节点</param>
    ''' <param name="element">元素名，非空时插入新元素，否则在该元素中插入属性</param>
    ''' <param name="attribute">属性名，非空时插入该元素属性值，否则插入元素值</param>
    ''' <param name="value">值</param>
    ''' <returns></returns>
    '*************************************************
    '         * 使用示列:
    '         * XmlHelper.Insert(path, "/Node", "Element", "", "Value")
    '         * XmlHelper.Insert(path, "/Node", "Element", "Attribute", "Value")
    '         * XmlHelper.Insert(path, "/Node", "", "Attribute", "Value")
    '         ***********************************************

    Public Shared Sub Insert(ByVal path As String, ByVal node As String, ByVal element As String, ByVal attribute As String, ByVal value As String)
        Try
            Dim doc As New XmlDocument()
            doc.Load(path)
            Dim xn As XmlNode = doc.SelectSingleNode(node)
            If element.Equals("") Then
                If Not attribute.Equals("") Then
                    Dim xe As XmlElement = DirectCast(xn, XmlElement)
                    xe.SetAttribute(attribute, value)
                End If
            Else
                Dim xe As XmlElement = doc.CreateElement(element)
                If attribute.Equals("") Then
                    xe.InnerText = value
                Else
                    xe.SetAttribute(attribute, value)
                End If
                xn.AppendChild(xe)
            End If
            doc.Save(path)
        Catch
        End Try
    End Sub

    ''' <summary>
    ''' 修改数据
    ''' </summary>
    ''' <param name="path">路径</param>
    ''' <param name="node">节点</param>
    ''' <param name="attribute">属性名，非空时修改该节点属性值，否则修改节点值</param>
    ''' <param name="value">值</param>
    ''' <returns></returns>
    '*************************************************
    '         * 使用示列:
    '         * XmlHelper.Insert(path, "/Node", "", "Value")
    '         * XmlHelper.Insert(path, "/Node", "Attribute", "Value")
    '         ***********************************************

    Public Shared Sub Update(ByVal path As String, ByVal node As String, ByVal attribute As String, ByVal value As String)
        Try
            Dim doc As New XmlDocument()
            doc.Load(path)
            Dim xn As XmlNode = doc.SelectSingleNode(node)
            Dim xe As XmlElement = DirectCast(xn, XmlElement)
            If attribute.Equals("") Then
                xe.InnerText = value
            Else
                xe.SetAttribute(attribute, value)
            End If
            doc.Save(path)
        Catch
        End Try
    End Sub

    ''' <summary>
    ''' 删除数据
    ''' </summary>
    ''' <param name="path">路径</param>
    ''' <param name="node">节点</param>
    ''' <param name="attribute">属性名，非空时删除该节点属性值，否则删除节点值</param>
    ''' <param name="value">值</param>
    ''' <returns></returns>
    '*************************************************
    '         * 使用示列:
    '         * XmlHelper.Delete(path, "/Node", "")
    '         * XmlHelper.Delete(path, "/Node", "Attribute")
    '         ***********************************************

    Public Shared Sub Delete(ByVal path As String, ByVal node As String, ByVal attribute As String)
        Try
            Dim doc As New XmlDocument()
            doc.Load(path)
            Dim xn As XmlNode = doc.SelectSingleNode(node)
            Dim xe As XmlElement = DirectCast(xn, XmlElement)
            If attribute.Equals("") Then
                xn.ParentNode.RemoveChild(xn)
            Else
                xe.RemoveAttribute(attribute)
            End If
            doc.Save(path)
        Catch
        End Try
    End Sub
End Class

