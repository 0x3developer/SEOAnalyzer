$(()=>{
    let btnAnalysis = $('#btn-analysis');

    let dataTable = $('#seo-data-table').DataTable(
        {
            "order": [[2, "desc"]],
            "searching": false,
            "paging": false,
            "info": false,
            "aoColumnDefs": [
                { "bSortable": false, "aTargets": [0] },
                { "bSearchable": false, "aTargets": [] }
            ]
        }
    );

    dataTable.on('order.dt search.dt', function () {
        dataTable.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();

    let linkTable = $('#seo-link-table').DataTable(
        {
            "order": [[1, "desc"]],
            "searching": false,
            "paging": false,
            "info": false,
            "aoColumnDefs": [
                { "bSortable": false, "aTargets": [0] },
                { "bSearchable": false, "aTargets": [] }
            ]
        }
    );

    linkTable.on('order.dt search.dt', function () {
        linkTable.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();

    SEOAnalyzer.Initializer(dataTable, linkTable);
    btnAnalysis.on('click', ()=>{
        if (SEOAnalyzer.IsUrlRequest)
        {
            SEOAnalyzer.AnalyzeUrl();
        } else {
            SEOAnalyzer.AnalyzeText();
        }
    })

});

let SEOAnalyzer =
{
    Inputs: {},
    EndPoints: {},
    IsUrlRequest:false,
    DataTable: {},
    LinkDataTable: {},
    AddDataTableData: (bodyData, metaData) => {

        let table = SEOAnalyzer.DataTable;
        table.clear().draw(false);

        if (bodyData != null)
        {
            for (let i = 0; i < bodyData.length; i++) {
                table.row.add([
                    i + 1,
                    bodyData[i].Word,
                    bodyData[i].Count,
                    "False"
                ]);
            } 
        }
        
        if (metaData != null) {
            for (let i = 0; i < metaData.length; i++) {
                table.row.add([
                    i + 1,
                    metaData[i].Word,
                    metaData[i].Count,
                    'True',
                ]);
            }
        }

        table.draw(false);
    },
    AddLinkTableData: (linksData) => {

        let table = SEOAnalyzer.LinkDataTable;
        table.clear().draw(false);

        if (linksData != null) {
            for (let i = 0; i < linksData.length; i++) {
                table.row.add([
                    i + 1,
                    linksData[i]
                ]);
            }
        }

        table.draw(false);
    },
    Initializer: (dataTable, linkDataTable) => {
        SEOAnalyzer.DataTable = dataTable;
        SEOAnalyzer.LinkDataTable = linkDataTable;


        SEOAnalyzer.EndPoints =
        {
            TextAnalyzerUrl: '/SEOAnalyzer/Text',
            HtmlAnalyzerUrl: '/SEOAnalyzer/Html',
            UrlAnalyzerUrl: '/SEOAnalyzer/Url'
        };

        SEOAnalyzer.Inputs = {
            TextInput: $('#input-text'),
            UrlInput: $('#input-url'),
            HtmlInput: $('#input-html'),

            ExcludeBodyInput: $('#chk-exclude-body'),
            ExcludeMetaInput: $('#chk-exclude-meta'),
            ExcludeStopWordsInput: $('#chk-exclude-stop-words'),
            ExcludeNumbersInput: $('#chk-exclude-numbers'),
            FindExternalLinksInput: $('#chk-find-external-links'),

            InputGroups: $('#input-groups'),
        }
        
        SEOAnalyzer.EnableSelected()
        $('input[name="seo-input-options"]').on('click', ()=>  SEOAnalyzer.EnableSelected())
        
    },
    EnableSelected: ()=>
    {
        SEOAnalyzer.Inputs.InputGroups.children().hide();
        let opt = $('input[name="seo-input-options"]:checked');
        SEOAnalyzer.IsUrlRequest = opt.val() == "input-url-group";
        $(`#${opt.val()}`).show()
    },
    AnalyzeUrl: () => {

        SEOAnalyzer.ResetForm();
        let url = SEOAnalyzer.Inputs.UrlInput.val();
        if (!SEOAnalyzer.ValidateInput(url))
            return false;

        SEOAnalyzer.ShowLoading(true);
        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: SEOAnalyzer.EndPoints.UrlAnalyzerUrl,
            data: JSON.stringify({
                ExcludeMeta: SEOAnalyzer.Inputs.ExcludeMetaInput.is(':checked'),
                ExcludeStopWords: SEOAnalyzer.Inputs.ExcludeStopWordsInput.is(':checked'),
                ExcludeBody: SEOAnalyzer.Inputs.ExcludeBodyInput.is(':checked'),
                ExcludeNumbers: SEOAnalyzer.Inputs.ExcludeNumbersInput.is(':checked'),
                FindExternalLinks: SEOAnalyzer.Inputs.FindExternalLinksInput.is(':checked'),
                url
            }),
            success: (data) => {
                if (data.Success == true) {
                    SEOAnalyzer.AddDataTableData(data.Data.BodyWordOccurrences, data.Data.MetaWordOccurrences)
                    SEOAnalyzer.AddLinkTableData(data.Data.ExternalLinks);
                } else {
                    SEOAnalyzer.ShowErrors(data.Error.Message);
                }
                SEOAnalyzer.ShowLoading(false);
            },
            error: () => {
                SEOAnalyzer.ShowErrors("Unknown Error");
                SEOAnalyzer.ShowLoading(false);
            },
        });
    },
    AnalyzeText: () => {

        SEOAnalyzer.ResetForm();

        let text = SEOAnalyzer.Inputs.TextInput.val();

        if (!SEOAnalyzer.ValidateInput(text))
            return false;

        SEOAnalyzer.ShowLoading(true);
        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: SEOAnalyzer.EndPoints.TextAnalyzerUrl,
            data: JSON.stringify({
                ExcludeMeta : SEOAnalyzer.Inputs.ExcludeMetaInput.is(':checked'),
                ExcludeStopWords: SEOAnalyzer.Inputs.ExcludeStopWordsInput.is(':checked'),
                ExcludeBody: SEOAnalyzer.Inputs.ExcludeBodyInput.is(':checked'),
                ExcludeNumbers: SEOAnalyzer.Inputs.ExcludeNumbersInput.is(':checked'),
                text,
            }),
            success: (data) =>
            {
                if (data.Success == true)
                {
                   SEOAnalyzer.AddDataTableData(data.Data.WordOccurrences)
                } else {
                    SEOAnalyzer.ShowErrors(data.Error.Message);
                }
                SEOAnalyzer.ShowLoading(false);
            },
            error:() => {
                SEOAnalyzer.ShowErrors("Unknown Error");
                SEOAnalyzer.ShowLoading(false);
            },
          });
    },
    ValidateInput: (text) => {
        if (['', undefined, null].includes(text)) {
            SEOAnalyzer.ShowErrors('Invalid input data');
            return false;
        }

        return true;
    },
    ShowErrors: (error)=>
    {
        $('#formError').text(error)
    },
    ShowLoading: (show) => {
        $('#loading-icon').LoadingOverlay(show ? "show" : "hide");
    },
    ResetForm: () => {
        $('#formError').text('');
        SEOAnalyzer.DataTable.clear().draw(false);
        SEOAnalyzer.LinkDataTable.clear().draw(false);
    },    
}