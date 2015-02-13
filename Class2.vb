Imports System.Data.SqlClient
Public Class DB
    ''' <summary>  
    ''' 获取局域网内的所有数据库服务器名称  
    ''' </summary>  
    ''' <returns>服务器名称数组</returns> 

    Public Function GetSqlServerNames() As List(Of String)
        Dim dataSources As DataTable = SqlClientFactory.Instance.CreateDataSourceEnumerator().GetDataSources()

        Dim column As DataColumn = dataSources.Columns("InstanceName")
        Dim column2 As DataColumn = dataSources.Columns("ServerName")

        Dim rows As DataRowCollection = dataSources.Rows
        Dim Serverlist As New List(Of String)()
        Dim array As String = String.Empty
        For i As Integer = 0 To rows.Count - 1
            Dim str2 As String = TryCast(rows(i)(column2), String)
            Dim str As String = TryCast(rows(i)(column), String)
            If ((str Is Nothing) OrElse (str.Length = 0)) OrElse ("MSSQLSERVER" = str) Then
                array = str2
            Else
                array = str2 & "/" & str
            End If

            Serverlist.Add(array)
        Next

        Serverlist.Sort()

        Return Serverlist
    End Function
    ''' <summary>  
    ''' 查询sql中的非系统库  
    ''' </summary>  
    ''' <param name="connection"></param>  
    ''' <returns></returns>  
    Public Function databaseList(ByVal connection As String) As List(Of String)
        Dim getCataList As New List(Of String)()
        Dim cmdStirng As String = "select name from sys.databases where database_id > 4"
        Dim connect As New SqlConnection(connection)
        Dim cmd As New SqlCommand(cmdStirng, connect)
        Try
            If connect.State = ConnectionState.Closed Then
                connect.Open()
                Dim dr As IDataReader = cmd.ExecuteReader()
                getCataList.Clear()
                While dr.Read()
                    getCataList.Add(dr("name").ToString())
                End While
                dr.Close()

            End If
            'MessageBox.Show(e.Message);  
        Catch e As SqlException
        Finally
            If connect IsNot Nothing AndAlso connect.State = ConnectionState.Open Then
                connect.Dispose()
            End If
        End Try
        Return getCataList
    End Function
    ''' <summary>  
    ''' 获取列名  
    ''' </summary>  
    ''' <param name="connection"></param>  
    ''' <returns></returns>  
    Public Function GetTables(ByVal connection As String) As DataTable
        Dim objTable As DataTable
        Dim objConnetion As New SqlConnection(connection)
        Try
            If objConnetion.State = ConnectionState.Closed Then
                objConnetion.Open()
                objTable = objConnetion.GetSchema("Tables")
            End If

        Catch
        Finally
            If objConnetion IsNot Nothing AndAlso objConnetion.State = ConnectionState.Closed Then
                objConnetion.Dispose()

            End If
        End Try
        Return objTable
    End Function
    ''' <summary>  
    ''' 获取字段  
    ''' </summary>  
    ''' <param name="connection"></param>  
    ''' <param name="TableName"></param>  
    ''' <returns></returns>  
    Public Function GetColumnField(ByVal connection As String, ByVal TableName As String) As DataTable

        Dim objConnetion As New SqlConnection(connection)
        Try
            If objConnetion.State = ConnectionState.Closed Then
                objConnetion.Open()
            End If
            Dim sda As New SqlDataAdapter
            Dim cmd As New SqlCommand("Select top 1 *  FROM " & TableName & " ", objConnetion)
            sda.SelectCommand = cmd
            Dim dt As New DataTable(TableName)
            sda.Fill(dt)
            Return dt
        Catch
        End Try


    End Function

End Class
