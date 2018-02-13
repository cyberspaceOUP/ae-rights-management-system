
var totalLakTrigrngDom = [];
(function ($) {
   var settings;
    $.fn.laksDocUpload = function (options) {
        //debugger;
        if (typeof this === "undefined" || $(this).length <= 0) {
            return false;
        }
        totalLakTrigrngDom[totalLakTrigrngDom.length] = $(this);
        this._defaults = {
            triggeringDom: this,
            containerElement: null,
            uploadPath: null,
            multiFile: false,
            fileUploadControlId: null,
            uploadedImgsClass: null,
            animate: false,
            replaceLast: false,
            width: -1,
            height: -1,
            allowedExtensions: null,
            fileSize: -1,
            progressBarContainer: null,
            progressBars: null,
            onUpClick: null,
            onInit: null,
            onBegin: null,
            onEachFileComplete: null,
            onAllUploadComplete: null,
            onUploadDelete: null,
            onEachFile: null,
            onBeforeFileSend: null,
            //getVariables: null,
            allowPreview: false,
            previewWidth: 0,
            previewHeight: 0,
            previewContainer: null,
            isMobile: null, //initiate as false
            isDragNDrop: false,
            DragNDropWidth: null, //in px or %
            DragNDropHeight: null, //in px or %
            progressBarClass: null
        }
        settings = $.extend({}, this._defaults, options);
        // device detection
        if (settings.isMobile == null && (/(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|ipad|iris|kindle|Android|Silk|lge |maemo|midp|mmp|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino/i.test(navigator.userAgent)
            || /1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-/i.test(navigator.userAgent.substr(0, 4)))) {
            settings.isMobile = true
        }
        else { settings.isMobile = false; };
        if (settings.isDragNDrop != true && settings.isDragNDrop != false) {
            settings.isDragNDrop = false;
        }
        laksUploadInit();
    }
    function laksUploadInit() {
        //alert($(settings.domElement).attr("class"));
        if (settings.isDragNDrop && !settings.isMobile) {
            fn_CreateDragNDrop();
        }
        var FUControlExists = false;
        var elementIdentifier = null;
        var elementId_class;
        $(settings.triggeringDom).on("click", function (event) {
            //debugger;
            fn_triggerUpload(this);
            event.preventDefault();
        });
        if (typeof $(settings.containerElement).attr("id") !== "undefined") {
            if ($(settings.containerElement).attr("id") != null) {
                elementIdentifier = "id";
                elementId_class = $(settings.containerElement).attr("id");
            }
        }
        if (elementIdentifier == null) {
            if (typeof $(settings.containerElement).attr("class") !== "undefined") {
                if ($(settings.containerElement).attr("class") != null) {
                    elementIdentifier = "class";
                    elementId_class = $(settings.containerElement).attr("class");
                }
            }
        }

        if (settings.fileUploadControlId != null && typeof settings.fileUploadControlId !== "undefined" && $(settings.fileUploadControlId).length > 0) {
            FUControlExists = true;
            $(settings.fileUploadControlId).bind("change", function () {
                fn_laksUploadsChange(this);
            })
            $(settings.fileUploadControlId).attr((settings.multiFile == false ? "" : "multiple"));
            $(settings.fileUploadControlId).attr("elementIdentifier", elementIdentifier);
            $(settings.fileUploadControlId).attr("containerElement", elementId_class);
            if (settings.isMobile) {
                $(settings.fileUploadControlId).css('display', 'block');
            }
        }
        if (FUControlExists == false) {
            FUControlExists = true;
            elementIdentifier = "id";
            var totalImgUploader = $("body").find("input[type$='file']").length;
            elementId_class = "div_laksDocUploader" + totalImgUploader.toString();
            
            $($(settings.triggeringDom).parent()).prepend("<div class='laksDocUploader' id='div_laksDocUploader" + totalImgUploader.toString() +
                                                            "' style='display:" + (settings.isMobile ? "block" : "none") + ";'></div>");
            if (settings.isDragNDrop && !settings.isMobile) {
                $(settings.triggeringDom).parent().find("#div_laksDocUploader" + totalImgUploader.toString()).css(
                    {
                        top: "-" + settings.dragBox.offset().top + "px",
                        left: "-" + settings.dragBox.offset().left + "px",
                        position: "absolute"
                    });
            }
            $("#div_laksDocUploader" + totalImgUploader.toString()).append("<input id='inp_laksDocUploader" + totalImgUploader.toString() + "' type='file' value='Upload'" +
                                                    (settings.multiFile == false ? "" : "multiple='multiple'") +
                                                    " onchange='return fn_laksUploadsChange(this);'" +
                                                    "elementIdentifier='" + elementIdentifier + "'" +
                                                    "containerElement='" + elementId_class + "'" +
                                                    "/>");
            settings.fileUploadControlId = $("input[id='inp_laksDocUploader" + totalImgUploader.toString() + "']");
            
        }
        if (settings.isMobile && FUControlExists) {

            

            $(settings.triggeringDom).css("display", "none");
            $(settings.fileUploadControlId).data("fileUploadControlData", { triggerer: settings.triggeringDom });
            $(settings.fileUploadControlId).on("click", function (EventClick) {
                $($(this).data("fileUploadControlData").triggerer).trigger("click");
                if (!VarConfirmationsIfMobile) {
                    EventClick.preventDefault();
                    VarConfirmationsIfMobile = true;
                }
            });
        }
        if (settings.onInit != null && typeof settings.onInit !== "undefined") {
            settings.onInit(settings);
        }
        //   debugger;
        
        $(settings.triggeringDom).data("lakOptionData", settings);
    }
    function fn_CreateDragNDrop() {
        var totalDropZone = $(".dropZone").length;
        if (settings.DragNDropWidth == null) {
            settings.DragNDropWidth = "300px";
        }
        if (settings.DragNDropHeight == null) {
            settings.DragNDropHeight = "200px";
        }
        var dragBox = $("<div></div>",
            {
                "class": "dropZone",
                "id": "dropZone" + totalDropZone,
                "style": "width:" + settings.DragNDropWidth + "; height:" + settings.DragNDropHeight + ";"
            });

        dragBox.insertBefore(settings.triggeringDom);
        settings = $.extend({}, settings, { dragBox: dragBox });
        settings.DragNDropWidth = $(dragBox).width(); // get calculated width
        settings.DragNDropHeight = $(dragBox).height(); // get calculated height
        for (indx = 0; indx < 3; indx++) {
            var htmlToSet = (indx == 0 ? "Drop files here to start uploading" : (indx == 1 ? "Or" : ""));
            $(dragBox).append(
                    $("<div></div>",
                        {
                            "style": "width:100%; height:" + (settings.DragNDropHeight / 3) + "px;" + (indx != 2 ? "line-height:" + (settings.DragNDropHeight / 3) + "px;" : "")
                        }).html(htmlToSet));
        }
        $(settings.triggeringDom).appendTo($(dragBox).find("div:nth-child(3)"));
        //  debugger;
       
        $(settings.triggeringDom).data("lakOptionData", settings);
        if (typeof $("#dropZoneTag") === "undefined" || $("#dropZoneTag").length <= 0) {
            var cssDragNDrop = "<style id='dropZoneTag'>" +
                           ".dropZone{" +
                               "position: relative;" +
                               "border: 2px dashed rgba(0,0,0,.3);" +
                               "border-radius: 20px;" +
                               "font-family: Arial;" +
                               "text-align: center;" +
                               "font-size: 18px;" + //20px
                               "color: rgba(0,0,0,.3);" +
                           "}" +
                           ".dropZone:hover{" +
                                "border: 2px dashed rgba(0,0,0,.5);" +
                                "color: rgba(0,0,0,.5);" +
                            "}" +
                            ".dropZone + .dropZone{" +
                                "margin-top:15px;" +
                            "}" +
                       "</style>";
            $("body").prepend(cssDragNDrop);
        }
        dragBox.data("triggererDom", settings.triggeringDom);
        dragBox[0].addEventListener("dragover",
            function (e) { fn_OnDragOver(e); }, true);

        dragBox[0].addEventListener("drop",
            function (e) {
                //alert("okay");
                fn_OnDrop(e);
            },
            true);
        fn_HandleWindowResizeForDragNDrop();
        $(window).resize(function () {
            fn_HandleWindowResizeForDragNDrop();
        });
    }
}
)(jQuery);
function fn_OnDragOver(e) {
    e.preventDefault();
    e.stopPropagation();
    var crrntDropBox = $(e.target);
    if (!crrntDropBox.hasClass("dropZone")) {
        crrntDropBox = $(crrntDropBox.parents(".dropZone"));
    }
    if (typeof crrntDropBox === "undefined" || crrntDropBox.length <= 0) {
        return false
    }
    var inputFile = crrntDropBox.find("input[type='file']");
    if (typeof inputFile === "undefined" || $(inputFile).length <= 0) {
        return false
    }
    var x = e.pageX;
    var y = e.pageY;
    //console.log((x - crrntDropBox.offset().left) + " and " + y);
    // return;
    inputFile = $(inputFile);
    inputFile.parent().css({ "display": "block", "opacity": "0" });
    inputFile.css({ "margin-left": (x - inputFile.width() / 2 - 50) + "px", "margin-top": (y - inputFile.height() / 2) + "px", "position": "absolute" });
}

var AllSettings;
var crrnt_triggeringDom;
var VarConfirmationsIfMobile = true;
var DropEvent = null;
var confrmAutoDelete = false;
var FilesFromDropEvent = null;
var crrnt_container;
var mint_totalFilesProgress = 0;
var xhrequest = null;
var allAttributes;
function fn_OnDrop(e) {
    var crrntDropBox = $(e.target);
    if (!crrntDropBox.hasClass("dropZone")) {
        crrntDropBox = $(crrntDropBox.parents(".dropZone"));
    }
    if (typeof crrntDropBox === "undefined" || crrntDropBox.length <= 0) {
        return false;
    }
    var triggererDom = crrntDropBox.data("triggererDom");
    if (typeof triggererDom === "undefined" || $(triggererDom).length <= 0) {
        return false;
    }
    DropEvent = e;
    FilesFromDropEvent = DropEvent.dataTransfer.files;
    e.preventDefault();
    e.stopPropagation();
   // debugger;
    var crrntAllSettings = $(triggererDom).data("lakOptionData");
    if (crrntAllSettings.multiFile == false && FilesFromDropEvent.length > 1) {
        alert("Upload single file only");
        if (DropEvent != null) {
            DropEvent.preventDefault();
        }
        FilesFromDropEvent = null;
        xhrequest = null;
        DropEvent = null;
        return false;
    }
    fn_triggerUpload(triggererDom);
}

function fn_HandleWindowResizeForDragNDrop() {

   
    $(totalLakTrigrngDom).each(function () {
       // debugger;
        var DragNDropSettings = $(this).data("lakOptionData");
        DragNDropSettings.DragNDropWidth = $(DragNDropSettings.dragBox).width(); // get calculated width
        DragNDropSettings.DragNDropHeight = $(DragNDropSettings.dragBox).height(); // get calculated height
        if ($(window).width() < 370) {
            varDom = $(DragNDropSettings.dragBox).find("div:nth-child(1)");
            varDom.css({ "line-height": "25px", "padding-top": "10px" });
        }
        var padding_left_trggr = 0;// (DragNDropSettings.DragNDropWidth - $(DragNDropSettings.triggeringDom).width()) / 2;
        var padding_top_trggr = ((DragNDropSettings.DragNDropHeight / 3) - $(DragNDropSettings.triggeringDom).height()) / 2 - 10;
        if (padding_left_trggr < 5) {
            padding_left_trggr = 5;
        }
        if (padding_top_trggr < 5) {
            padding_top_trggr = 5;
        }
        $(DragNDropSettings.triggeringDom).css(
            {
                "margin-top": padding_top_trggr + "px",
                "margin-left": padding_left_trggr + "px"
            });
      //  debugger;
        $(this).data("lakOptionData", DragNDropSettings);
    });
}

function fn_triggerUpload(UploadTrigger) {
  //  debugger;
  
    crrnt_triggeringDom = UploadTrigger;
    
    AllSettings = $(UploadTrigger).data("lakOptionData");
   
   
    if (AllSettings.onUpClick != null && typeof AllSettings.onUpClick !== "undefined") {
        AllSettings.onUpClick(UploadTrigger);
       
    }
    if (AllSettings.replaceLast == true && AllSettings.multiFile == false && AllSettings.progressBars != null && AllSettings.progressBars.length > 0) {
        
        if (!confrmAutoDelete) {
            alert("This will delete old upload").set("onok", function () {
                confrmAutoDelete = true
                fn_triggerUpload(UploadTrigger);
            });
            if (DropEvent != null) {
                DropEvent.preventDefault();
            }
            return false;
        }
        if (confrmAutoDelete) {
            if (typeof AllSettings.containerElement !== "undefined" && $(AllSettings.containerElement).length > 0) {
                $(AllSettings.containerElement).html("");
            }
            $(AllSettings.progressBars).find(".crossUpload").trigger("click");
        }
        else {
            VarConfirmationsIfMobile = false;
            return false;
        }
        confrmAutoDelete = false;
    }
    if (!AllSettings.isMobile && DropEvent == null) {
        
       
        
        $(AllSettings.fileUploadControlId).trigger("click");


    }
    else if (!AllSettings.isMobile && DropEvent != null) {
       
        fn_laksUploadsChange($(AllSettings.fileUploadControlId)[0]);
    }
}

function fn_laksUploadsChange(crrnt_Uploader) {


    if (AllSettings.onBegin != null && typeof AllSettings.onBegin !== "undefined") {
        AllSettings.onBegin(AllSettings);

    }
    var elementIdentifier = $(crrnt_Uploader).attr("elementIdentifier");
    var containerElement = $(crrnt_Uploader).attr("containerElement");
    if (typeof containerElement !== "undefined" && typeof elementIdentifier !== "undefined") {
        crrnt_container = $((elementIdentifier == "class" ? "." : "#") + containerElement);
        if (typeof AllSettings !== "undefined") {
            flUp_Documents = crrnt_Uploader; ///Global variable
            var allFiles = crrnt_Uploader.files;
            allFiles = (FilesFromDropEvent != null ? FilesFromDropEvent : allFiles);
            allFiles = allFiles;
            if (typeof allFiles === "undefined") {
                return false;
            }
            var totalFiles = allFiles.length;
            if (AllSettings.multiFile == false && totalFiles > 1) {
                alert("Upload single file only");
                if (DropEvent != null) {
                    DropEvent.preventDefault();
                }
                FilesFromDropEvent = null;
                xhrequest = null;
                DropEvent = null;
                laksResetFileUploader();
                return false;
            }
            var progresstableNo = $("*[progresstableNo]");
            if (typeof progresstableNo === "undefined" || $(progresstableNo).length == 0) {
                progresstableNo = 1;
            }
            else {
                progresstableNo = $(progresstableNo).length + 1;
            }
            for (indx = 0; indx < totalFiles; indx++) {
                var extn = allFiles[indx].name.split(".").pop().toLowerCase();
                var allowedExtension = (AllSettings.allowedExtensions == null ? extn : AllSettings.allowedExtensions).toLowerCase().split(",");
                if (allowedExtension.indexOf(extn) < 0 || extn == "exe" || extn == "msi") {
                    alert("Extension '" + extn.toUpperCase() + "' is not allowed, Please upload valid document with extension" +
                                            (allowedExtension.length > 1 ? "s " : " ")
                                            + (extn == "exe" || extn == "msi" ? "other than '" : "'")
                                            + allowedExtension.join().toUpperCase() + "'");
                    FilesFromDropEvent = null;
                    xhrequest = null;
                    DropEvent = null;
                    laksResetFileUploader();
                    return false;
                }
                var crrnt_FileSize = allFiles[indx].size;
                if (typeof crrnt_FileSize !== "undefined" && AllSettings.fileSize > -1 && (crrnt_FileSize / ((1024 * 1024)*10)) > AllSettings.fileSize) {
                    var ShowDataLimit = (AllSettings.fileSize < 1 ? AllSettings.fileSize * 1000 + " KB" : AllSettings.fileSize*10 + " MB");
                    alert("Please upload file with size less than " + ShowDataLimit);
                    xhrequest = null;
                    FilesFromDropEvent = null;
                    DropEvent = null;
                    laksResetFileUploader();
                    return false;
                }
            }
            for (indx = 0; indx < totalFiles; indx++) {
                if (AllSettings.onEachFile != null && typeof AllSettings.onEachFile !== "undefined") {
                    AllSettings.onEachFile(AllSettings, indx);
                }
                var progressBarElement = $("<div></div>",
                                        {
                                            "class": "progresstable" + (AllSettings.progressBarClass != null ? " " + AllSettings.progressBarClass : ""),
                                            "progresstableNo": (progresstableNo + indx),
                                            "id": "progresstable_" + (progresstableNo + indx)
                                        })
                                    .html(
                                         $("<div></div>",
                                        {
                                            "class": "td_1_uploadProgress",
                                            "tdType": "uploadProgress" + indx.toString()
                                        }).html("<div class='progress'><span class='meter upParent' style='width:0%'>&nbsp;</span></div>" + "<div class='crossUpload' onclick='return deleteUpload(this);'></div>"));
                $(progressBarElement).data("triggeringDom", AllSettings.triggeringDom);
                if (AllSettings.progressBarContainer == null) {
                    $(crrnt_triggeringDom).parent().append(progressBarElement);
                    $(crrnt_triggeringDom).parent().append("<div class='uploadedFileName'>Uploaded File:- <u class='uploadMultfileName'>" + allFiles[indx].name + "</u></div>");
                }
                else {
                    $(AllSettings.progressBarContainer).append(progressBarElement);
                    $(AllSettings.progressBarContainer).append("<div class='uploadedFileName'>Uploaded File:- <u class='uploadMultfileName'>" + allFiles[indx].name + "</u></div>");
                }
                if (AllSettings.progressBars == null) {
                    AllSettings.progressBars = [];
                }
                AllSettings.progressBars.push(progressBarElement[0]);
                if (!AllSettings.isMobile && AllSettings.isDragNDrop) {
                    AllSettings.DragNDropHeight = AllSettings.DragNDropHeight + $(progressBarElement).height() +
                                                    $($(progressBarElement).nextAll(".uploadedFileName")[0]).height() + 55;
                    $(AllSettings.dragBox).stop().animate({ "height": AllSettings.DragNDropHeight + "px" }, "fast", "linear");
                }
                //debugger;
                $(AllSettings.triggeringDom).data("lakOptionData", AllSettings);

                var crrntImgTempPath = null;
                var previewImgTag = null;
                if (extn.indexOf("png") >= 0 || extn.indexOf("jpg") >= 0 || extn.indexOf("jpeg") >= 0 || extn.indexOf("gif") >= 0) {

                    if (navigator.userAgent.search("Safari") >= 0 && navigator.userAgent.search("Chrome") < 0) {
                        //for safari browser
                        crrntImgTempPath = "~/uploads/" + allFiles[indx];
                    }
                    else {
                        //for all except safari browser
                        crrntImgTempPath = URL.createObjectURL(allFiles[indx]);
                    }
                }
                if (crrntImgTempPath != null && AllSettings.allowPreview != null && AllSettings.allowPreview == true &&
                            (AllSettings.previewContainer == null || typeof AllSettings.previewContainer === "undefined" ||
                                    $(AllSettings.previewContainer).length <= 0)) {
                    previewImgTag = $(crrnt_triggeringDom).parent().find("img[preview='true']");
                    if (typeof previewImgTag === "undefined" || previewImgTag.length <= 0) {
                        $(crrnt_triggeringDom).parent().append("<img src='' preview='true'/>");
                        previewImgTag = $(crrnt_triggeringDom).parent().find("img[preview='true']");
                    }
                }
                else if (crrntImgTempPath != null && AllSettings.allowPreview != null && AllSettings.allowPreview == true &&
                            AllSettings.previewContainer != null && typeof AllSettings.previewContainer !== "undefined") {
                    previewImgTag = $(AllSettings.previewContainer).find("img[preview='true']");
                    if (typeof previewImgTag === "undefined" || previewImgTag.length <= 0) {
                        $(AllSettings.previewContainer).append("<img src='' preview='true'/>");
                        previewImgTag = $(AllSettings.previewContainer).find("img[preview='true']");
                    }
                }
                if (crrntImgTempPath != null && previewImgTag != null) {
                    $(previewImgTag).attr("src", crrntImgTempPath);
                    $(previewImgTag).css({
                        "width": AllSettings.previewWidth,
                        "height": (AllSettings.previewHeight.replace(/\s+/g, "").trim() == "0px" ? "auto" : AllSettings.previewHeight)
                    });
                }
            }
            mint_totalFilesProgress = totalFiles;
            fn_laksUploader(allFiles, totalFiles);
        }
    }
}

function fn_laksUploader(allFiles, totalFiles) {
    if (fn_laksHandleUndefinedVariables(allFiles) == true) {
        for (file_index = 0; file_index < totalFiles ; file_index++) {
            if (AllSettings.onBeforeFileSend != null && typeof AllSettings.onBeforeFileSend !== "undefined") {
                AllSettings.onBeforeFileSend(AllSettings, totalFiles, file_index);
            }
            var crrnt_formData = new FormData();

            //crrnt_formData.append(allFiles[file_index].name, allFiles[file_index]);
            crrnt_formData.append("files", allFiles[file_index]); // "files" name should be same as parameter in MVC action 
            var crrnt_xhrequest = new XMLHttpRequest();
            crrnt_xhrequest.xmlIndx = (AllSettings.progressBars.length - totalFiles + file_index);
            crrnt_xhrequest.upload.xmlIndx = (AllSettings.progressBars.length - totalFiles + file_index);
            if (xhrequest == null) {
                xhrequest = [];
            }
            xhrequest[xhrequest.length] = crrnt_xhrequest;
            crrnt_xhrequest.upload.onprogress = function (event) {
                var crrnt_xhrequest_1 = this;
                var progress = Math.round(event.loaded * 100 / event.total);
                var crrnt_progress = $(AllSettings.progressBars[(Math.round(crrnt_xhrequest_1.xmlIndx))]);
                if (typeof crrnt_progress !== "undefined" && $(crrnt_progress).length > 0) {
                    $(crrnt_progress).find("span.upParent").stop().animate({ "width": progress + "%" }, "fast", "linear");
                }
            }
            crrnt_xhrequest.onreadystatechange = function () {
                var crrnt_xhrequest_1 = this;
                if (typeof crrnt_xhrequest_1 !== "undefined" && crrnt_xhrequest_1 != null && crrnt_xhrequest_1.readyState == 4 && crrnt_xhrequest_1.status == 200) {
                    var crrntProgressBarIndx = (Math.round(crrnt_xhrequest_1.xmlIndx));
                    var crrnt_progress = $(AllSettings.progressBars[crrntProgressBarIndx]);
                    if (typeof crrnt_progress !== "undefined" && $(crrnt_progress).length > 0) {
                        if (AllSettings.onEachFileComplete != null && typeof AllSettings.onEachFileComplete !== "undefined") {
                            AllSettings.onEachFileComplete(AllSettings, (crrnt_xhrequest_1.responseText).toString(), crrntProgressBarIndx);
                        }
                    }
                }
            }
            if (AllSettings.uploadPath != null) {
                crrnt_xhrequest.open("POST", AllSettings.uploadPath);

                crrnt_xhrequest.send(crrnt_formData);
            }
        }
        if (AllSettings.multiFile) {
            $(AllSettings.triggeringDom).val("Add More");
        }
        if (AllSettings.onAllUploadComplete != null && typeof AllSettings.onAllUploadComplete !== "undefined") {
            AllSettings.onAllUploadComplete(AllSettings);
        }
        xhrequest = null;
        DropEvent = null;
        FilesFromDropEvent = null;
    }
    laksResetFileUploader(); 
}

function laksResetFileUploader() {
    if (typeof flUp_Documents !== "undefined") {
        var crrnt_triggerer;
        if (typeof $(flUp_Documents).data("fileUploadControlData") != "undefined" && $(flUp_Documents).data("fileUploadControlData") != null &&
                    $(flUp_Documents).data("fileUploadControlData").hasOwnProperty('triggerer')) {
            crrnt_triggerer = $(flUp_Documents).data("fileUploadControlData").triggerer;
        }
        if ($(flUp_Documents).length > 0) {
            allAttributes = ($(flUp_Documents)[0]).attributes;
            flUp_Documents.value = null;
            $("input[id=" + $(flUp_Documents).attr("id") + "]").remove();
            flUp_Documents = null;
            if (typeof crrnt_container !== "undefined") {
                if ($(crrnt_container).length > 0) {
                    var newHtml = "<input ";
                    if (allAttributes != null) {
                        if (allAttributes.length > 0) {
                            $.each(allAttributes, function () {
                                if (this.specified) {
                                    newHtml = newHtml + " " + this.name + "='" + this.value + "' ";
                                }
                            })
                            newHtml = newHtml + " />";
                            $(crrnt_container).prepend(newHtml);
                            if (AllSettings.isMobile) {
                                var iop = $(crrnt_container).find("input[type=file]");
                                $(iop).data("fileUploadControlData", { triggerer: crrnt_triggerer });
                                $(iop).on("click", function (EventClick) {
                                    $($(this).data("fileUploadControlData").triggerer).trigger("click");
                                    if (!VarConfirmationsIfMobile) {
                                        EventClick.preventDefault();
                                        VarConfirmationsIfMobile = true;
                                    }
                                });
                            }
                            crrnt_container = undefined;
                        }
                    }
                }
            }
        }
    }
}

function deleteUpload(domElement) {
    //debugger;
    var crrnt_progresstable = $(domElement).parents(".progresstable");
    var triggeringDom = crrnt_progresstable.data("triggeringDom");
    var crrntSettings = $(triggeringDom).data("lakOptionData");
    if (xhrequest != null) {
        var prgsIndx = $(crrntSettings.progressBars).indexOf(crrnt_progresstable);
        xhrequest.each(function () {
            if (this.xmlIndx !== undefined && Math.round(this.xmlIndx) == prgsIndx) {
                xhrequest.splice(prgsIndx, 1);
                this.abort();
            }
        });
    }
    var resetHeight = (!crrntSettings.isMobile && crrntSettings.isDragNDrop);
    var onUploadDelete = crrntSettings.onUploadDelete;
    crrntSettings.progressBars = $(crrntSettings.progressBars).not(crrnt_progresstable);
    crrnt_progresstable.find("span").animate({ "width": "0%" }, "fast", function () {
        if (onUploadDelete != null && typeof onUploadDelete !== "undefined") {
            AllSettings.DragNDropHeight -= 80; // added by prakash on 05 June, 2017
            onUploadDelete(domElement, AllSettings);
        }
        if (resetHeight) {
            crrntSettings.DragNDropHeight = crrntSettings.DragNDropHeight - ($(crrnt_progresstable.nextAll(".uploadedFileName")[0]).height() + crrnt_progresstable.height());
            if (crrntSettings.progressBars == null || crrntSettings.progressBars.length <= 0) {
                crrntSettings.DragNDropHeight = crrntSettings.DragNDropHeight - (crrntSettings.progressBars != null && crrntSettings.progressBars.length > 1 ? 45 : 0);
            }
            $(crrntSettings.dragBox).animate({ "height": crrntSettings.DragNDropHeight + "px" }, "fast", "linear");
          //  debugger;
            $(triggeringDom).data("lakOptionData", crrntSettings);
        }
        $(crrnt_progresstable.nextAll(".uploadedFileName")[0]).remove();
        $(crrnt_progresstable).remove();
    });
    if (!confrmAutoDelete) {//added code to check if second if alertify has been used
        //  laksResetFileUploader();
    }
}

function fn_laksHandleUndefinedVariables(obj) {
    var returnVal = false;
    if (typeof obj !== "undefined") {
        if (obj.length > 0) {
            returnVal = true;
        }
    }
    return returnVal;
}
