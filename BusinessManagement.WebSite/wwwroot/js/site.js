var NavMenu = function (isHideLogo, thisEl){
    var isHideLogo = isHideLogo;

    this.init = function () {
        navBarMenuState(thisEl, isHideLogo);
    
        $(window).scroll(function () {
            navBarMenuState(thisEl, isHideLogo);
        });
    }
}

function navBarMenuState(thisEl, isHideLogo) {
    var logoEl = $(".logo");
    var navbarEl = $(".nav-menu");


    if (thisEl.scrollTop() > 1) {
   
        if (isHideLogo) {
          logoEl.css("display", "table-cell");
        } 
        
        navbarEl.css("border-color", "#ccd5d7");
        navbarEl.addClass("fixed");
    } else {

        if (isHideLogo) {
            logoEl.hide();
        }       
        navbarEl.css("border-color", "rgba(255, 255, 255, 0.95)");
        navbarEl.removeClass("fixed");
    }
}


function ShowVideoHydra(videoContainer) {
    
    function startVideo() {
        var videoURL = $('#hydra-video-frame').prop('src');
        videoURL = videoURL.replace("autoplay=0", "autoplay=1");
        $('#hydra-video-frame').prop('src', videoURL);
    }

    function stopVideo() {
        var videoURL = $('#hydra-video-frame').prop('src');
        videoURL = videoURL.replace("autoplay=1", "autoplay=0");
        $("#hydra-video-frame").prop('src', '');
        $("#hydra-video-frame").prop('src', videoURL);
    }

    $.get("/home/HydraVideo", function (data) {


        if ($('#' + videoContainer).html() == "") {
            $('#' + videoContainer).html(data);
            $('#hydra-video-modal').appendTo("body").modal('show');

            startVideo();


            $("#hydra-video-modal").on("hidden.bs.modal", function () {
                stopVideo();
            });

        } else {
            $('#hydra-video-modal').appendTo("body").modal('show');
            startVideo();
        }

    });
}


function isValidFile(fileUploadId, typesStr) {
  var fuData = document.getElementById(fileUploadId);
  var fileUploadPath = fuData.value;

  if (fileUploadPath == '') {
    return false;

  } else {
    var extension = fileUploadPath.substring(
      fileUploadPath.lastIndexOf('.') + 1).toLowerCase();

    var types = typesStr.split(",");

    for (var i = 0; i < types.length; i++) {
      if (extension.toLowerCase().trim() == types[i].toLowerCase().trim()) {
        return true;
      }
    }
    return false;
  }
}

//////////////SHOW GLOBAL LOADING
function showLoading() {
  $('#loading').show();
}
//////////////HIDE GLOBAL LOADING
function hideLoading() {
  $('#loading').hide();
}
//////////////SHOW LOCAL LOADING
function showLoaderInEl(el) {
  $(el).append('<div class="custom-loader"><div class="spinner"><div class="bounce1"></div><div class="bounce2"></div><div class="bounce3"></div></div></div>');
}

//////////////HIDE GLOBAL LOADING
function hideLoaderInEl(el) {
  $(el).find('.custom-loader').remove();
}

////////////////////////////RELOAD DATA
var reloadData = function (obj) {
    debugger;
  var dataCon = $(obj.dataCon),
    url = obj.url,
    take = obj.take;

  var customParams = obj.customParams;

  var search = "";
  if (obj.searchInput != null) {
    search = $(obj.searchInput).val();
  }

    if (search != "") {
        search = "&searchString=" + search;
    }

  url = url + "?skip=0&take=" + take + search;


  if (customParams != null) {

    for (var i = 0; i < customParams.lenght; i++) {
      url += "&" + customParams[i].key + "=" + customParams[i].value;
    }
  }

  showLoaderInEl($(dataCon).parent());

$.ajax({
    type: "POST",
    url: url,
    cache: false,
    success: function (data) {

      dataCon.html(data);
        //hideLoaderInEl($(dataCon).parent());

        if (obj.dataLoaded != null) {
            obj.dataLoaded();
        }

    }
  });
}

//////////////////////////IS VALID SEARCH INPUT CHARACTER
function isValidCharacter(str) {
    var pattern = /[<>:"\/\\|?*\x00-\x1F]/;
    return !pattern.test(str.trim());
}

///////////////Search

function setSearch(obj) {

    var searchEl = obj.searchEl;
    var ulEl = obj.ulEl;
    var actionUrl = obj.actionUrl;
    var nodataMsg = obj.noDataNsg;
    var take = obj.take;

    if (nodataMsg == null) {
        nodataMsg = "No results!";
    }
    var delayTimer;


    function searchFinished() {
        if (obj.searchFinished != null) {
            obj.searchFinished();
        }
    }

    searchEl.on('keyup', function () {
        debugger;
        var val = searchEl.val().trim();
        if (!isValidCharacter(val)) {
            return false;
        }
        ulEl.attr('data-all', 0); // SET IS ALL DATA LOADED TO FALSE
        ulEl.attr('data-searching', 1); // SET IS IT DATA LOADED FROM SEARCH TO DISABLE SCROLLER DATA LOADING

        var loaderCont = ulEl.parent('div');
             
            if (actionUrl != null) {
                url = actionUrl;
            } else {
                url = $(input).closest('form').attr('action');
            }

            url += "?skip=" + 0 + "&take=" + take + "&searchString=" + val;
            if (obj.customParameters != null && obj.customParameters.length > 0) {

                for (var i = 0; i < obj.customParameters.length; i++) {
                    url += "&" + obj.customParameters[i].parameterName + "=" + $(obj.customParameters[i].fieldSelector).val();
                }
            }

            ulEl.empty(); //CLEAR ALL LOADED DATA
            showLoaderInEl(loaderCont);


            clearTimeout(delayTimer);

            delayTimer = setTimeout(function () {

                $.get(url, function (data) {
                    hideLoaderInEl(loaderCont);
                    ulEl.attr('data-searching', 0); // FINISHED LOADING DATA
                    if (data.trim() != "") {
                        ulEl.append(data);
                        searchFinished();
                    } else {
                        ulEl.append('<li>' + nodataMsg + '</li>');
                        searchFinished();
                    }
                });

            }, 1000);

        });
}



var CustomParameters = function (parameterName, fieldSelector) {
    this.parameterName = parameterName;
    this.fieldSelector = fieldSelector;
}

///////////////////////////INFINITY SCROLL
var setInfinityScroll = function (obj) {
    debugger;

    var take = obj.take,
        isLoading = false;


    var ulEl = $(obj.ulElSelector);

    var isWindowScroll = obj.isWindowScroll;

    var loaderCont = ulEl;

  /////////////////////////////////////////////COUNT ALL CURRENT LISTED ELEMENTS
  function setSkip() {
      return parseInt(ulEl.children('li').length);
  }

    function loadEjectedElements(skip, take) {
        debugger;
        var isAllDataLoaded = parseInt(ulEl.attr('data-all'));
        var isSearching = parseInt(ulEl.attr('data-searching'));
    if (isLoading || isAllDataLoaded == 1 || isSearching == 1) {
      return false;
    }


        showLoaderInEl("#" + loaderCont.attr("id"));
        $('.spinner').css('top', 'auto');
   
    isLoading = true;
        var search = $(obj.searchInput).val();

 
        var url = obj.url + "?skip=" + skip + "&take=" + take + "&searchString=" + search;

        if (obj.customParameters != null && obj.customParameters.length > 0) {

            for (var i = 0; i < obj.customParameters.length; i++) {
                url += "&" + obj.customParameters[i].parameterName + "=" + $(obj.customParameters[i].fieldSelector).val();
            }
        }

    $.ajax({
      url: url,
      cache: false,
      success: function (html) {

         ulEl.append(html);

        if (html.trim().length == 0 || html.trim().length == "") {
            ulEl.attr('data-all', 1);
        }

        isLoading = false;

          hideLoaderInEl(loaderCont);
          //if (isWindowScroll) {
          //    hideLoading();
          //} else {
             
          //}
      }
    });
  }

    bindInfinityScroll(obj.scrolledEl, function () { loadEjectedElements(setSkip(), take); }, isWindowScroll);
}


////////////////ADD SCROLL EVENT TO UL ELEMENT
function bindInfinityScroll(el, func, isWindowScroll) {
    debugger;


    if (isWindowScroll) {
        $(el).on('scroll', function () {
            if ($(window).scrollTop() > $(document).height() - $(window).height() - 100) {
                func();
            }
        });
    } else {
        $(el).on('scroll', function () {
            if ($(this).scrollTop() + $(this).innerHeight() >= $(this)[0].scrollHeight - 100) {
                func();
            }
        });
    }
}



///////////BOOTSTRAP MODAL////////////
$(window).on('shown.bs.modal', function () {
    $(".containerPerspective").css("position", "static");
    $(".nav-menu").css({'z-index': '8' });
    $(".note-toolbar").css({ 'z-index': '7' });
    $(".outer-nav").css({ 'z-index': '-1' });
});

$(window).on('hidden.bs.modal', function () {
    $(".containerPerspective").css("position", "relative");
    $(".nav-menu").css({'z-index': '' });
    $(".note-toolbar").css({ 'z-index': '500' });
    $(".outer-nav").css({ 'z-index': '' });
});