Public Class XMLForm
    Dim xmlL As New XmlHelper
    Dim xml As String = Application.StartupPath & ("/XmlFile.xml")

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        '插入元素
        XmlHelper.Insert(xml, nodepath.Text, elet.Text, att.Text, valuet.Text)
        '插入元素/属性
        'XmlHelper.Insert(xml, "/Root/Studio", "Site", "Name", "小路工作室");
        'XmlHelper.Insert(xml, "/Root/Studio", "Site", "Name", "丁香鱼工作室");
        'XmlHelper.Insert(xml, "/Root/Studio", "Site", "Name", "谱天城工作室");
        'XmlHelper.Insert(xml, "/Root/Studio/Site[@Name='谱天城工作室']", "Master", "", "红尘静思");
        '插入属性
        'XmlHelper.Insert(xml, "/Root/Studio/Site[@Name='小路工作室']", "", "Url", "http://www.wzlu.com/");
        'XmlHelper.Insert(xml, "/Root/Studio/Site[@Name='丁香鱼工作室']", "", "Url", "http://www.luckfish.net/");
        'XmlHelper.Insert(xml, "/Root/Studio/Site[@Name='谱天城工作室']", "", "Url", "http://www.putiancheng.com/");
        '修改元素值
        'XmlHelper.Update(xml, "/Root/Studio/Site[@Name='谱天城工作室']/Master", "", "RedDust");
        '修改属性值
        'XmlHelper.Update(xml, "/Root/Studio/Site[@Name='谱天城工作室']", "Url", "http://www.putiancheng.net/");
        'XmlHelper.Update(xml, "/Root/Studio/Site[@Name='谱天城工作室']", "Name", "PuTianCheng Studio");
        '读取元素值
        'Response.Write("<div>" + XmlHelper.Read(xml, "/Root/Studio/Site/Master", "") + "</div>");
        '读取属性值
        'Response.Write("<div>" + XmlHelper.Read(xml, "/Root/Studio/Site", "Url") + "</div>");
        '读取特定属性值
        'Response.Write("<div>" + XmlHelper.Read(xml, "/Root/Studio/Site[@Name='丁香鱼工作室']", "Url") + "</div>");
        '删除属性
        'XmlHelper.Delete(xml, "/Root/Studio/Site[@Name='小路工作室']", "Url");
        '删除元素
        'XmlHelper.Delete(xml, "/Root/Studio", "");
    End Sub

    Private Sub XMLForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        MsgBox(XmlHelper.Read(xml, "/Root/UI/code", "nvarchar"))

    End Sub
End Class