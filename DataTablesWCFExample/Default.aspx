<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DataTablesWCFExample._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link rel="Stylesheet" href="Content/datatable.css" />
<link rel="Stylesheet" href="Scripts/ui/css/redmond/jquery-ui-1.7.2.custom.css" />
<script src="Scripts/jquery-1.4.4.min.js"></script>
<script src="Scripts/jquery.dataTables.js"></script>
<script src="Scripts/jquery.json-2.2.min.js"></script>
<script src="Scripts/ui/jquery-ui-1.7.2.custom.min.js"></script>
    <title></title>
    <script>
        $(document).ready(function() {
            var oArticleListTable;
            var $gridId = "articlelistview-grid";
            $("#<%=divArticleListGrid.ClientID %>").html('<table cellpadding="0" cellspacing="0" border="0" class="data-table-container w-fill" id="' + $gridId + '"></table>');
            oArticleListTable = $("#" + $gridId).dataTable({
                "bProcessing": true,
                "bJQueryUI": true,
                "bAutoWidth": false,
                "bServerSide": true,
                "oLanguage": { "sProcessing": "<div class='processing-div ui-state-focus w120 ui-corner-all'><div style='float:left;'><img src='/content/img/icons/loading_32x32.gif'></div><div style='float:left;margin:10px 5px;'>Loading...</div><div class='cleardiv'></div></div>" },
                "sAjaxSource": "/webservices/ArticleListService.svc/GetArticles",
                "sPaginationType": "full_numbers",
                //"fnInitComplete": ArticleListTable_Init,
                "asStripClasses": ['even', 'odd ui-state-default'],
                "aaSorting": [[1, "asc"]],
                "aoColumns": [
                        { "sTitle": "PromoImageId", "sClass": "id-column grid-cell" },
                        { "sTitle": "ArticleId", "sClass": "grid-cell" },
			            { "sTitle": "Slug", "sClass": "grid-cell" },
			            { "sTitle": "Title", "sClass": "grid-cell" },
			            { "sTitle": "StatusId", "sClass": "grid-cell" },
			            { "sTitle": "PublicationDate", "sClass": "grid-cell" }
		             ],
                "fnServerData": function(sSource, aoData, fnCallback) {
                    aoData.push({ "name": "websiteId", "value": "70005" });
                    aoData.push({ "name": "categoryId", "value": null });

                    $.ajax({
                        "dataType": 'json',
                        "type": "GET",
                        "url": sSource,
                        "data": aoData,
                        "success": function(msg) {
                            var json = $.evalJSON(msg.d);
                            fnCallback(json);
                        }
                    });
                }
            });
            function ArticleListTable_Init(oSettings) {
                $(nNodes).each(function() {
                    $(this).hover(function() {
                        $(this).addClass("ui-state-active row-hover");
                    }, function() {
                        $(this).removeClass("ui-state-active row-hover");
                    })
                });
            }
        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div id="divArticleListGrid" runat="server"></div>
    </div>
    </form>
</body>
</html>
