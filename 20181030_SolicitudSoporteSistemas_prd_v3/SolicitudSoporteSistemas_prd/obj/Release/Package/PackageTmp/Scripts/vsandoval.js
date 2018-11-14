/* vsandoval.js v0.0.1
 * Copyright © 2017 Victor Sandoval Norambueba
 * Free to use under the WTFPL license.
 * victorssandovaln@gmail.com
 */



$(function () {


    $(".dropdown-menu li a").click(function () {
        var selText = "", selVal = "";
        selText = $(this).text();
        selVal = $(this).parent().attr("id");  // funciona

        $(this).parents('.dropdown').find('.dropdown-toggle').html(selText + ' <span class="caret"></span>');
        $(this).parents('.dropdown').find('.dropdown-toggle').css({ "background-color": "#999999 !important" });

        $(this).parents('.dropdown').find('li').removeClass("active");
        $(this).parent().addClass('active');

        //var selVal2 = $(this).parent().selectedIndex;
        //var selVal3 = $(this).parent().parent().selectedIndex;
        //$(this).parents('.dropdown').find('.text-center').html(selVal2 + ' <span class="caret"></span>');
    });

    //$(".dropdown-menu li a").click(function () {

    //    $(".btn-default:first-child").text($(this).text());
    //    $(".btn-default:first-child").val($(this).text());
    //    $(".btn-default:first-child").css({ "background-color": "#999999 !important" });

    //    //$(this).text($(this).text());
    //    //$(this).val($(this).text());
    //    //$(this).css({ "background-color": "#999999 !important" });

    //});

    //$(".btn dropdown-toggle").click(function () {
    //    $(".dropdown-toggle").text($(this).text());
    //    $(".dropdown-toggle").val($(this).text());
    //    $(".dropdown-toggle").css({ "background-color": "#999999 !important" });
    //});

    // We can attach the `fileselect` event to all file inputs on the page
    $(document).on('change', ':file', function () {
        var input = $(this),
            numFiles = input.get(0).files ? input.get(0).files.length : 1,
            label = input.val().replace(/\\/g, '/').replace(/.*\//, '');
        input.trigger('fileselect', [numFiles, label]);
    });

    // We can watch for our custom `fileselect` event like this
    $(document).ready(function () {
        $(':file').on('fileselect', function (event, numFiles, label) {

            var input = $(this).parents('.input-group').find(':text'),
                log = numFiles > 1 ? numFiles + ' documentos' : label;

            if (input.length) {
                input.val(log);
            } else {
                if (log) alert(log);
            }

        });
    });


    // typeahead
    $(document).ready(function () {

        // para Prueba.cshtml
        if (document.getElementById("pruebapersonalid") == null) {
        }
        else {
            var auxiliar = document.getElementById("pruebapersonalid");
            //auxiliar.type = "text";
            var otro = auxiliar.value;
            otro = otro.replace(/'/g, '"');
            var arrayprueba = JSON.parse("[" + otro + "]");

            $('#la-prueba .typeahead').typeahead({
                hint: true,
                highlight: true,
                minLength: 1
            },
                {
                    name: 'auxiliar',
                    //display: 'prueba',
                    //empty: "NA",
                    source: substringMatcher(arrayprueba)
                });
        }


        if (document.getElementById("listacuentasid") == null) {
        }
        else {
            var cuentas = document.getElementById("listacuentasid");
            //auxiliar.type = "text";
            var lista_cuentas = cuentas.value;
            lista_cuentas = lista_cuentas.replace(/'/g, '"');
            var array_lista_cuentas = JSON.parse("[" + lista_cuentas + "]");

            var token_cuentas = new Bloodhound({
                //datumTokenizer: Bloodhound.tokenizers.obj.whitespace('email', 'nombre'),

                datumTokenizer: function (ds) {

                    var uno = Bloodhound.tokenizers.whitespace(ds.email);
                    var dos = Bloodhound.tokenizers.whitespace(ds.nombre);
                    //return uno.concat(dos);

                    //var test_ds = Bloodhound.tokenizers.whitespace(ds.email, ds.nombre);
                    var test_ds = uno.concat(dos);

                    $.each(test_ds, function (k, v) {
                        i = 0;
                        while ((i + 1) < v.length) {
                            test_ds.push(v.substr(i, v.length));
                            i++;
                        }
                    })
                    return test_ds;
                },

                queryTokenizer: Bloodhound.tokenizers.whitespace,
                local: array_lista_cuentas
            });

            token_cuentas.initialize();


            // Solicitante
            //if (document.getElementById("solicitanteemailid") != null) {
            //    $('#divsolicitanteemail .typeahead').typeahead(
            //        {
            //            hint: true,
            //            highlight: true,
            //            minLength: 1
            //        }, {
            //            name: 'solicitante',
            //            displayKey: 'email',
            //            source: token_cuentas.ttAdapter(),
            //            limit: 10,
            //            templates: {
            //                empty: [
            //                    '<div class="empty-message">',
            //                    'No existe Solicitante en el Active Directory.',
            //                    '</div>'
            //                ].join('\n'),
            //                suggestion: function (data) {
            //                    return '<div>' + data.email + '– <em>' + data.nombre + '</em></div>'
            //                }
            //            }
            //        }).on('typeahead:selected', function (event, data) {
            //            $('#divsolicitanteemail .typeahead').val(data.email).trigger('keyup');

            //            if (document.getElementById("solicitantenombreid") != null) {
            //                $('#solicitantenombreid').val(data.nombre).trigger('keyup');
            //            }
            //        });;

            //    var myVal1 = $('#solicitanteemailid').val();
            //    $('#divsolicitanteemail .typeahead').val(myVal1).trigger('keyup');
            //    //$('#modificar_solicitanteemailid').typeahead('val', myVal).trigger('keyup');

            //    if (document.getElementById("solicitantenombreid") != null) {
            //        var myVal2 = $('#solicitantenombreid').val();
            //        $('#solicitantenombreid').val(myVal2).trigger('keyup');
            //    }
            //}


            // Técnico
            if (document.getElementById("asignadoemailid") != null) {
                $('#divasignadoemail .typeahead').typeahead(
                    {
                        hint: true,
                        highlight: true,
                        minLength: 1
                    }, {
                        name: 'solicitante',
                        displayKey: 'email',
                        source: token_cuentas.ttAdapter(),
                        limit: 10,
                        templates: {
                            empty: [
                                '<div class="empty-message">',
                                'No existe Solicitante en el Active Directory.',
                                '</div>'
                            ].join('\n'),
                            suggestion: function (data) {
                                return '<div>' + data.email + '– <em>' + data.nombre + '</em></div>'
                            }
                        }
                    }).on('typeahead:selected', function (event, data) {
                        $('#divasignadoemail .typeahead').val(data.email).trigger('keyup');
                    });;

                var myVal2 = $('#asignadoemailid').val();
                $('#divasignadoemail .typeahead').val(myVal2).trigger('keyup');
                //$('#modificar_solicitanteemailid').typeahead('val', myVal).trigger('keyup');
            }

        }


        if (document.getElementById("listasolicitantesid") == null) {
        }
        else {
            var solicitantes = document.getElementById("listasolicitantesid");
            //auxiliar.type = "text";
            var lista_solicitantes = solicitantes.value;
            lista_solicitantes = lista_solicitantes.replace(/'/g, '"');
            var array_lista_solicitantes = JSON.parse("[" + lista_solicitantes + "]");

            var token_solicitantes = new Bloodhound({
                //datumTokenizer: Bloodhound.tokenizers.obj.whitespace('email', 'nombre'),

                datumTokenizer: function (ds) {

                    var uno = Bloodhound.tokenizers.whitespace(ds.nombrecompleto);
                    var dos = Bloodhound.tokenizers.whitespace(ds.cargo);
                    var tres = Bloodhound.tokenizers.whitespace(ds.email);
                    var cuatro = Bloodhound.tokenizers.whitespace(ds.nickname);
                    var cinco = Bloodhound.tokenizers.whitespace(ds.direccion);
                    var seis = Bloodhound.tokenizers.whitespace(ds.empresaid);
                    var siete = Bloodhound.tokenizers.whitespace(ds.telefono);
                    var ocho = Bloodhound.tokenizers.whitespace(ds.ubicacion);
                    //return uno.concat(dos);

                    //var test_ds = Bloodhound.tokenizers.whitespace(ds.email, ds.nombre);
                    var test_ds = uno.concat(dos).concat(tres).concat(cuatro).concat(cinco).concat(seis).concat(siete).concat(ocho);

                    $.each(test_ds, function (k, v) {
                        i = 0;
                        while ((i + 1) < v.length) {
                            test_ds.push(v.substr(i, v.length));
                            i++;
                        }
                    })
                    return test_ds;
                },
                queryTokenizer: Bloodhound.tokenizers.whitespace,
                local: array_lista_solicitantes
            });

            token_solicitantes.initialize();

            if (document.getElementById("solicitanteemailid") != null) {

                // Proyecto -> carga otros campos
                $('#divsolicitanteemail .typeahead').typeahead(
                    {
                        hint: true,
                        highlight: true,
                        minLength: 1
                    }, {
                        name: 'solicitante',
                        displayKey: 'email',
                        source: token_solicitantes.ttAdapter(),
                        limit: 10,
                        templates: {
                            empty: [
                                '<div class="empty-message">',
                                'No existe Solicitante. ',
                                '</div>'
                            ].join('\n'),
                            suggestion: function (data) {
                                return '<div>' + data.email + '– <em>' + data.nombrecompleto + '</em></div>'
                            }
                        }
                    }).on('typeahead:selected', function (event, data) {

                        $('#divsolicitanteemail .typeahead').val(data.email).trigger('keyup');

                        // crear SSS Rápida

                        if (document.getElementById("crearsss_ubicacionid") != null) {
                            $('#crearsss_ubicacionid').typeahead('val', data.ubicacion).trigger('keyup');
                        }

                        // crear SSS Completa

                        if (document.getElementById("solicitantenombreid") != null) {
                            $('#solicitantenombreid').val(data.nombrecompleto).trigger('keyup');
                        }

                        if (document.getElementById("crearsss_telefonoid") != null) {
                            $('#crearsss_telefonoid').val(data.telefono).trigger('keyup');
                        }

                        if (document.getElementById("crearsss_direccionid") != null) {
                            $('#crearsss_direccionid').val(data.direccion).trigger('keyup');
                        }

                        if (document.getElementById("crearsss_empresaid") != null) {
                            $('#crearsss_empresaid').val(data.empresaid).trigger('keyup');
                        }

                        // modificar SSS

                        if (document.getElementById("modificarsss_ubicacionid") != null) {
                            $('#modificarsss_ubicacionid').typeahead('val', data.ubicacion).trigger('keyup');
                        }

                        if (document.getElementById("modificarsss_solicitantenombreid") != null) {
                            $('#modificarsss_solicitantenombreid').val(data.nombrecompleto).trigger('keyup');
                        }

                        if (document.getElementById("modificarsss_telefonoid") != null) {
                            $('#modificarsss_telefonoid').val(data.telefono).trigger('keyup');
                        }

                        if (document.getElementById("modificarsss_direccionid") != null) {
                            $('#modificarsss_direccionid').val(data.direccion).trigger('keyup');
                        }

                        if (document.getElementById("modificarsss_empresaid") != null) {
                            $('#modificarsss_empresaid').val(data.empresaid).trigger('keyup');
                        }
                    });


                var myVal0 = $('#solicitanteemailid').val();
                $('#divsolicitanteemail .typeahead').val(myVal0).trigger('keyup');


                if (document.getElementById("crearsss_ubicacionid") != null) {
                    var myVal1 = $('#crearsss_ubicacionid').val();
                    $('#crearsss_ubicacionid').val(myVal1).trigger('keyup');
                }

                if (document.getElementById("solicitantenombreid") != null) {
                    var myVal2 = $('#solicitantenombreid').val();
                    $('#solicitantenombreid').val(myVal2).trigger('keyup');
                }

                if (document.getElementById("crearsss_telefonoid") != null) {
                    var myVal3 = $('#crearsss_telefonoid').val();
                    $('#crearsss_telefonoid').val(myVal3).trigger('keyup');
                }

                if (document.getElementById("crearsss_direccionid") != null) {
                    var myVal4 = $('#crearsss_direccionid').val();
                    $('#crearsss_direccionid').val(myVal4).trigger('keyup');
                }

                if (document.getElementById("crearsss_empresaid") != null) {
                    var myVal5 = $('#crearsss_empresaid').val();
                    $('#crearsss_empresaid').val(myVal5).trigger('keyup');
                }



                if (document.getElementById("modificarsss_ubicacionid") != null) {
                    var myVal6 = $('#modificarsss_ubicacionid').val();
                    $('#modificarsss_ubicacionid').val(myVal6).trigger('keyup');
                }

                if (document.getElementById("modificarsss_solicitantenombreid") != null) {
                    var myVal7 = $('#modificarsss_solicitantenombreid').val();
                    $('#modificarsss_solicitantenombreid').val(myVal7).trigger('keyup');
                }

                if (document.getElementById("modificarsss_telefonoid") != null) {
                    var myVal8 = $('#modificarsss_telefonoid').val();
                    $('#modificarsss_telefonoid').val(myVal8).trigger('keyup');
                }

                if (document.getElementById("modificarsss_direccionid") != null) {
                    var myVal9 = $('#modificarsss_direccionid').val();
                    $('#modificarsss_direccionid').val(myVal9).trigger('keyup');
                }

                if (document.getElementById("modificarsss_empresaid") != null) {
                    var myVal10 = $('#modificarsss_empresaid').val();
                    $('#modificarsss_empresaid').val(myVal10).trigger('keyup');
                }

            }
        }



        $('#solicitanteemailid').change(function () {

            var myValSolEm = $('#solicitanteemailid').val();

            // Crear

            if (document.getElementById("crearsss_ubicacionid") != null) {
                var myValCreUb = $('#crearsss_ubicacionid').val();

                if (myValSolEm != null && myValSolEm != '') {
                    //$('#empresarazonsocialid').val(myValRaz).trigger('keyup');
                    $('#crearsss_ubicacionid').typeahead('val', myValCreUb).trigger('keyup');
                } else {
                    //$('#empresarazonsocialid').val(null).trigger('keyup');
                    $('#crearsss_ubicacionid').typeahead('val', null).trigger('keyup');
                }
            }

            if (document.getElementById("solicitantenombreid") != null) {
                var myValCreNo = $('#solicitantenombreid').val();

                if (myValSolEm != null && myValSolEm != '') {
                    $('#solicitantenombreid').val(myValCreNo).trigger('keyup');
                } else {
                    $('#solicitantenombreid').val(null).trigger('keyup');
                }
            }

            if (document.getElementById("crearsss_telefonoid") != null) {
                var myValCreTe = $('#crearsss_telefonoid').val();

                if (myValSolEm != null && myValSolEm != '') {
                    $('#crearsss_telefonoid').val(myValCreTe).trigger('keyup');
                } else {
                    $('#crearsss_telefonoid').val(null).trigger('keyup');
                }
            }

            if (document.getElementById("crearsss_direccionid") != null) {
                var myValCreDi = $('#crearsss_direccionid').val();

                if (myValSolEm != null && myValSolEm != '') {
                    $('#crearsss_direccionid').val(myValCreDi).trigger('keyup');
                } else {
                    $('#crearsss_direccionid').val(null).trigger('keyup');
                }
            }

            if (document.getElementById("crearsss_empresaid") != null) {
                var myValCreEm = $('#crearsss_empresaid').val();

                if (myValSolEm != null && myValSolEm != '') {
                    $('#crearsss_empresaid').val(myValCreEm).trigger('keyup');
                } else {
                    $('#crearsss_empresaid').val(null).trigger('keyup');
                }
            }


            // Modificar

            if (document.getElementById("modificarsss_ubicacionid") != null) {
                var myValModUb = $('#modificarsss_ubicacionid').val();

                if (myValSolEm != null && myValSolEm != '') {
                    //$('#empresarazonsocialid').val(myValRaz).trigger('keyup');
                    $('#modificarsss_ubicacionid').typeahead('val', myValModUb).trigger('keyup');
                } else {
                    //$('#empresarazonsocialid').val(null).trigger('keyup');
                    $('#modificarsss_ubicacionid').typeahead('val', null).trigger('keyup');
                }
            }

            if (document.getElementById("modificarsss_solicitantenombreid") != null) {
                var myValModNo = $('#modificarsss_solicitantenombreid').val();

                if (myValSolEm != null && myValSolEm != '') {
                    $('#modificarsss_solicitantenombreid').val(myValModNo).trigger('keyup');
                } else {
                    $('#modificarsss_solicitantenombreid').val(null).trigger('keyup');
                }
            }

            if (document.getElementById("modificarsss_telefonoid") != null) {
                var myValModTe = $('#modificarsss_telefonoid').val();

                if (myValSolEm != null && myValSolEm != '') {
                    $('#modificarsss_telefonoid').val(myValModTe).trigger('keyup');
                } else {
                    $('#modificarsss_telefonoid').val(null).trigger('keyup');
                }
            }

            if (document.getElementById("modificarsss_direccionid") != null) {
                var myValModDi = $('#modificarsss_direccionid').val();

                if (myValSolEm != null && myValSolEm != '') {
                    $('#modificarsss_direccionid').val(myValModDi).trigger('keyup');
                } else {
                    $('#modificarsss_direccionid').val(null).trigger('keyup');
                }
            }

            if (document.getElementById("modificarsss_empresaid") != null) {
                var myValModEm = $('#modificarsss_empresaid').val();

                if (myValSolEm != null && myValSolEm != '') {
                    $('#modificarsss_empresaid').val(myValModEm).trigger('keyup');
                } else {
                    $('#modificarsss_empresaid').val(null).trigger('keyup');
                }
            }


        });






        if (document.getElementById("listaubicacionesid") == null) {
        }
        else {
            var aux_crearsss_ubicacion = document.getElementById("listaubicacionesid");
            //auxiliar.type = "text";
            var otro_csssu = aux_crearsss_ubicacion.value;
            otro_csssu = otro_csssu.replace(/'/g, '"');
            var arrayubicacioncrearsss = JSON.parse("[" + otro_csssu + "]");

            $('#divubicacionsss .typeahead').typeahead({
                hint: true,
                highlight: true,
                minLength: 1
            },
                {
                    name: 'ubicacion',
                    //display: 'prueba',
                    //empty: "NA",
                    source: substringMatcher(arrayubicacioncrearsss)
                });

            if (document.getElementById("modificarsss_ubicacionid") != null) {
                var myVal = $('#modificarsss_ubicacionid').val();
                $('#divubicacionsss .typeahead').val(myVal).trigger('keyup');
            }
        }


        // para CrearSSS.cshtml - empresas
        var empresas = ['Almagro', 'EmpresasSocovesa', 'Mollendo', 'Montgras', 'Pilares', 'Selar', 'Socoicsa', 'Socovesa'
        ];

        $('#divempresa .typeahead').typeahead({
            hint: true,
            highlight: true,
            minLength: 1
        },
            {
                name: 'empresas',
                source: substringMatcher(empresas)
            });


        if (document.getElementById("listaccypid") == null) {
        }
        else {
            var aux_crearsss_ccyp = document.getElementById("listaccypid");
            //auxiliar.type = "text";
            var otro_ccyp = aux_crearsss_ccyp.value;
            otro_ccyp = otro_ccyp.replace(/'/g, '"');
            var arrayccypcrearsss = JSON.parse("[" + otro_ccyp + "]");

            $('#divccypsss .typeahead').typeahead({
                hint: true,
                highlight: true,
                minLength: 1
            },
                {
                    name: 'ccyp',
                    //display: 'prueba',
                    //empty: "NA",
                    source: substringMatcher(arrayccypcrearsss)
                });

            if (document.getElementById("modificarsss_ccypid") != null) {
                var myVal = $('#modificarsss_ccypid').val();
                $('#divccypsss .typeahead').val(myVal).trigger('keyup');
            }
        }


        if (document.getElementById("listasocid") == null) {
        }
        else {
            var aux_crearsss_soc = document.getElementById("listasocid");
            //auxiliar.type = "text";
            var otro_soc = aux_crearsss_soc.value;
            otro_soc = otro_soc.replace(/'/g, '"');
            var arraysoccrearsss = JSON.parse("[" + otro_soc + "]");

            $('#divsociedadsss .typeahead').typeahead({
                hint: true,
                highlight: true,
                minLength: 1
            },
                {
                    name: 'soc',
                    //display: 'prueba',
                    //empty: "NA",
                    source: substringMatcher(arraysoccrearsss)
                });

            if (document.getElementById("modificarsss_sociedadid") != null) {
                var myVal = $('#modificarsss_sociedadid').val();
                $('#divsociedadsss .typeahead').val(myVal).trigger('keyup');
            }
        }


        // para CrearSSSComp.cshtml - carga subcategoria segun categoria
        if (document.getElementById("sss_categoriaid") == null) {
        }
        else {
            $("#sss_categoriaid").change(function () {
                var idModel = $(this).val();
                if (idModel > 0) {
                    $.getJSON("/SS/CargarSubByCat", { id: idModel },
                        function (carData) {
                            var select = $("#sss_subcategoriaid");
                            select.empty();
                            select.append($('<option/>', {
                                value: 0,
                                text: "Seleccione Sub-categoría"
                            }));
                            $.each(carData, function (index, itemData) {

                                select.append($('<option/>', {
                                    value: itemData.Value,
                                    text: itemData.Text
                                }));
                            });
                        });
                }
            });
        }



    });


    // para ModificarSSS.cshtml - cargar button de dropbox
    $(document).ready(function () {

        // Empresa
        if (document.getElementById("modificarsss_empresa_li") == null) {
        }
        else {
            // modificarsss_estado_li_activo
            var mempresalia = document.getElementById("modificarsss_empresa_li");
            var mempresalia_text = mempresalia.innerText;

            if (document.getElementById("modificarsss_empresa_button") == null) {
            }
            else {
                //$('#modificarsss_estado_button').text(mestadolia_text);
                $('#modificarsss_empresa_button').html(mempresalia_text + ' <span class="caret"></span>');
                $('#modificarsss_empresa_button').css({ "background-color": "#999999 !important" });
            }
        }

        // Centro Costo
        if (document.getElementById("modificarsss_centrocosto_li") == null) {
        }
        else {
            // modificarsss_estado_li_activo
            var mcentrocostolia = document.getElementById("modificarsss_centrocosto_li");
            var mcentrocostolia_text = mcentrocostolia.innerText;

            if (document.getElementById("modificarsss_centrocosto_button") == null) {
            }
            else {
                //$('#modificarsss_estado_button').text(mestadolia_text);
                $('#modificarsss_centrocosto_button').html(mcentrocostolia_text + ' <span class="caret"></span>');
                $('#modificarsss_centrocosto_button').css({ "background-color": "#999999 !important" });
            }
        }


        // Tipo Solicitud
        if (document.getElementById("modificarsss_tiposolicitud_li") == null) {
        }
        else {
            // modificarsss_estado_li_activo
            var mtiposolicitudlia = document.getElementById("modificarsss_tiposolicitud_li");
            var mtiposolicitudlia_text = mtiposolicitudlia.innerText;

            if (document.getElementById("modificarsss_tiposolicitud_button") == null) {
            }
            else {
                //$('#modificarsss_estado_button').text(mestadolia_text);
                $('#modificarsss_tiposolicitud_button').html(mtiposolicitudlia_text + ' <span class="caret"></span>');
                $('#modificarsss_tiposolicitud_button').css({ "background-color": "#999999 !important" });
            }
        }

        // Estado
        if (document.getElementById("modificarsss_estado_li") == null) {
        }
        else {
            // modificarsss_estado_li_activo
            var mestadolia = document.getElementById("modificarsss_estado_li");
            var mestadolia_text = mestadolia.innerText;

            if (document.getElementById("modificarsss_estado_button") == null) {
            }
            else {
                //$('#modificarsss_estado_button').text(mestadolia_text);
                $('#modificarsss_estado_button').html(mestadolia_text + ' <span class="caret"></span>');
                $('#modificarsss_estado_button').css({ "background-color": "#999999 !important" });
            }
        }

        // Categoria
        if (document.getElementById("modificarsss_categoria_li") == null) {
        }
        else {
            // modificarsss_estado_li_activo
            var mcategorialia = document.getElementById("modificarsss_categoria_li");
            var mcategorialia_text = mcategorialia.innerText;

            if (document.getElementById("modificarsss_categoria_button") == null) {
            }
            else {
                //$('#modificarsss_estado_button').text(mestadolia_text);
                $('#modificarsss_categoria_button').html(mcategorialia_text + ' <span class="caret"></span>');
                $('#modificarsss_categoria_button').css({ "background-color": "#999999 !important" });
            }
        }

        // Sub Categoria
        if (document.getElementById("modificarsss_subcategoria_li") == null) {
        }
        else {
            // modificarsss_estado_li_activo
            var msubcategorialia = document.getElementById("modificarsss_subcategoria_li");
            var msubcategorialia_text = msubcategorialia.innerText;

            if (document.getElementById("modificarsss_subcategoria_button") == null) {
            }
            else {
                //$('#modificarsss_estado_button').text(mestadolia_text);
                $('#modificarsss_subcategoria_button').html(msubcategorialia_text + ' <span class="caret"></span>');
                $('#modificarsss_subcategoria_button').css({ "background-color": "#999999 !important" });
            }
        }

    });


    // para CrearSSS.cshtml - cargar button de dropbox
    $(document).ready(function () {

        // Estado
        if (document.getElementById("crearsss_estado_li") == null) {
        }
        else {
            // modificarsss_estado_li_activo
            var cestadolia = document.getElementById("crearsss_estado_li");
            var cestadolia_text = cestadolia.innerText;

            if (document.getElementById("crearsss_estado_button") == null) {
            }
            else {
                //$('#crearsss_estado_button').text(mestadolia_text);
                $('#crearsss_estado_button').html(cestadolia_text + ' <span class="caret"></span>');
                $('#crearsss_estado_button').css({ "background-color": "#999999 !important" });
            }
        }
    });


    // para CrearSSSComp.cshtml - cargar calendarios
    $(document).ready(function () {

        if (document.getElementById("datePicker") == null) {
        }
        else {
            $("#datePicker").datepicker({
                //showOn: 'both',
                //buttonImage: 'calendar.png',
                //buttonImageOnly: true,
                //changeYear: true,
                //numberOfMonths: 2
                format: 'dd/mm/yyyy',
                startDate: '01/01/2018',
                endDate: '30/12/3020',
                autoclose: true,
                weekStart: 1,
                monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
                monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
                dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
                dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],
                dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
                weekHeader: 'Sm',
                dateFormat: 'dd/mm/yy',
                firstDay: 1,
                isRTL: false,
                showMonthAfterYear: false,
                yearSuffix: ''
            });
        }

        if (document.getElementById("crearsss_fechacomprometidaid") == null) {
        }
        else {
            $("#crearsss_fechacomprometidaid").datepicker({
                //showOn: 'both',
                //buttonImage: 'calendar.png',
                //buttonImageOnly: true,
                //changeYear: true,
                //numberOfMonths: 2
                format: 'dd/mm/yyyy',
                startDate: '01/01/2018',
                endDate: '30/12/3020',
                autoclose: true,
                weekStart: 1,
                monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
                monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
                dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
                dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],
                dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
                weekHeader: 'Sm',
                dateFormat: 'dd/mm/yy',
                firstDay: 1,
                isRTL: false,
                showMonthAfterYear: false,
                yearSuffix: ''
            });
        }


        if (document.getElementById("crearsss_fechasolicitadaid") == null) {
        }
        else {
            $("#crearsss_fechasolicitadaid").datepicker({
                //showOn: 'both',
                //buttonImage: 'calendar.png',
                //buttonImageOnly: true,
                //changeYear: true,
                //numberOfMonths: 2
                format: 'dd/mm/yyyy',
                startDate: '01/01/2018',
                endDate: '30/12/3020',
                autoclose: true,
                weekStart: 1,
                monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
                monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
                dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
                dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],
                dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
                weekHeader: 'Sm',
                dateFormat: 'dd/mm/yy',
                firstDay: 1,
                isRTL: false,
                showMonthAfterYear: false,
                yearSuffix: ''
            });
        }

    });


    // para ModificarSSS.cshtml - cargar calendarios
    $(document).ready(function () {

        if (document.getElementById("datePicker") == null) {
        }
        else {
            $("#datePicker").datepicker({
                //showOn: 'both',
                //buttonImage: 'calendar.png',
                //buttonImageOnly: true,
                //changeYear: true,
                //numberOfMonths: 2
                format: 'dd/mm/yyyy',
                startDate: '01/01/2018',
                endDate: '30/12/3020',
                autoclose: true,
                weekStart: 1,
                monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
                monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
                dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
                dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],
                dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
                weekHeader: 'Sm',
                dateFormat: 'dd/mm/yy',
                firstDay: 1,
                isRTL: false,
                showMonthAfterYear: false,
                yearSuffix: ''
            });
        }

        if (document.getElementById("modificarsss_fechacomprometidaid") == null) {
        }
        else {
            $("#modificarsss_fechacomprometidaid").datepicker({
                //showOn: 'both',
                //buttonImage: 'calendar.png',
                //buttonImageOnly: true,
                //changeYear: true,
                //numberOfMonths: 2
                format: 'dd/mm/yyyy',
                startDate: '01/01/2018',
                endDate: '30/12/3020',
                autoclose: true,
                weekStart: 1,
                monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
                monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
                dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
                dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],
                dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
                weekHeader: 'Sm',
                dateFormat: 'dd/mm/yy',
                firstDay: 1,
                isRTL: false,
                showMonthAfterYear: false,
                yearSuffix: ''
            });
        }


        if (document.getElementById("modificarsss_fechasolicitadaid") == null) {
        }
        else {
            $("#modificarsss_fechasolicitadaid").datepicker({
                //showOn: 'both',
                //buttonImage: 'calendar.png',
                //buttonImageOnly: true,
                //changeYear: true,
                //numberOfMonths: 2
                format: 'dd/mm/yyyy',
                startDate: '01/01/2018',
                endDate: '30/12/3020',
                autoclose: true,
                weekStart: 1,
                monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
                monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
                dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
                dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],
                dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
                weekHeader: 'Sm',
                dateFormat: 'dd/mm/yy',
                firstDay: 1,
                isRTL: false,
                showMonthAfterYear: false,
                yearSuffix: ''
            });
        }

    });



    // para CrearSSS.cshtml - se quitan estados finales
    $(document).ready(function () {

        if (document.getElementById("crearsss_estadoid") == null) {
        }
        else {
            $("#crearsss_estadoid option[value='3']").remove();
            $("#crearsss_estadoid option[value='4']").remove();
            $("#crearsss_estadoid option[value='5']").remove();
            $("#crearsss_estadoid option[value='7']").remove();
        }

    });


    // para CrearSSSComp.cshtml - se quitan estados finales
    $(document).ready(function () {

        if (document.getElementById("crearssscomp_estadoid") == null) {
        }
        else {
            $("#crearssscomp_estadoid option[value='5']").remove();
            $("#crearssscomp_estadoid option[value='7']").remove();
        }
    });


    // para CrearLogSSS.cshtml
    $(document).ready(function () {

        if (document.getElementById("crearlogsss_listaminutosid") == null) {
        }
        else {
            $("#crearlogsss_listaminutosid").change(function () {

                if (document.getElementById("crearlogsss_horasid") == null) {
                }
                else {

                    //alert($(this).val());                                 // Funciona
                    //alert($('#crearlogsss_listaminutosid').val());        // Funciona
                    //alert($('#crearlogsss_listaminutosid').Value);        // No funciona (indefinido)

                    //alert($('#crearlogsss_listaminutosid').val() / 60);   // Funciona

                    var aux_logsss_horas = document.getElementById("crearlogsss_horasid");

                    if ($('#crearlogsss_listaminutosid').val() > 0) {
                        aux_logsss_horas.value = ($('#crearlogsss_listaminutosid').val() / 60).toFixed(2).toString().replace(".", ",");      // Funciona
                        //$('#crearlogsss_horasid').val() = $('#crearlogsss_listaminutosid').val() / 60;        // No funciona (error)
                        //$('#crearlogsss_horasid').value = $('#crearlogsss_listaminutosid').val() / 60;        // No funciona (sin error y sin mostrar calculo)
                    }


                }

            });
        }


        if (document.getElementById("crearlogsss_listatipologsid") == null) {
        }
        else {
            $("#crearlogsss_listatipologsid option[value='1']").remove();
            $("#crearlogsss_listatipologsid option[value='6']").remove();
            $("#crearlogsss_listatipologsid option[value='8']").remove();
        }


    });



    /* # Validando Formulario de pagina validador.html ============================================*/
    $(document).ready(function () {
        $('#formulario').validate({
            errorElement: "span",
            rules: {
                txtNombre: {
                    minlength: 2,
                    required: true
                },
                txtEmail: {
                    required: true,
                    email: true
                },
                txtTitulo: {
                    minlength: 2,
                    required: true
                },
                txtDescripcion: {
                    minlength: 2,
                    required: true
                }
            },
            highlight: function (element) {
                $(element).closest('.control-group')
                    .removeClass('success').addClass('error');
            },
            success: function (element) {
                element
                    .text('OK!').addClass('help-inline')
                    .closest('.control-group')
                    .removeClass('error').addClass('success');
            }
        });
    });


    /* # Validando Formulario de pagina crearsss.html ============================================*/
    $(document).ready(function () {
        $('#crearsolicitudsoportesistemas').validate({
            errorElement: "span",
            rules: {
                //crearsss_solicitante: {
                //    minlength: 2,
                //    required: true
                //},
                //crearsss_ubicacion: {
                //    minlength: 2,
                //    required: true
                //},
                crearsss_descripcion: {
                    minlength: 2,
                    required: true
                }
            },
            highlight: function (element) {
                $(element).closest('.form-group')
                    .removeClass('success').addClass('error');
            },
            success: function (element) {
                element
                    .text('').addClass('help-inline')
                    .closest('.form-group')
                    .removeClass('error').addClass('success');
            }
        });
    });


    /* # Validando Formulario de pagina desplegarsolicitudsoportesistemas.html ============================================*/
    $(document).ready(function () {
        $('#crearcomentariolog').validate({
            errorElement: "span",
            rules: {
                comentariolog: {
                    minlength: 2,
                    required: true
                }
            },
            highlight: function (element) {
                $(element).closest('.form-group')
                    .removeClass('success').addClass('error');
            },
            success: function (element) {
                element
                    .text('').addClass('help-inline')
                    .closest('.form-group')
                    .removeClass('error').addClass('success');
            }
        });
    });

    /* # Validando campo descripcion Log en Formulario de pagina solicitudsoportesistemas.html ============================================*/
    $(document).ready(function () {
        $('#item_crearcomentariolog').validate({
            errorElement: "span",
            rules: {
                icomentariolog: {
                    minlength: 2,
                    required: true
                }
            },
            highlight: function (element) {
                $(element).closest('.form-group')
                    .removeClass('success').addClass('error');
            },
            success: function (element) {
                element
                    .text('').addClass('help-inline')
                    .closest('.form-group')
                    .removeClass('error').addClass('success');
            }
        });
    });

    /* # Validando campo descripcion Log en Formulario de pagina solicitudsoportesistemas.html ============================================*/
    $(document).ready(function () {
        $('#item_crearcomentariolog').validate({
            errorElement: "span",
            rules: {
                icomentariolog: {
                    minlength: 2,
                    required: true
                }
            },
            highlight: function (element) {
                $(element).closest('.form-group')
                    .removeClass('success').addClass('error');
            },
            success: function (element) {
                element
                    .text('').addClass('help-inline')
                    .closest('.form-group')
                    .removeClass('error').addClass('success');
            }
        });
    });


    $(document).ready(function () {
        $('#myTable').dataTable({
            ordering: false
        });

        $('#myTableLogs').dataTable({
            //ordering: false,
            "order": [[1, "desc"]]
        });

        $('#myTableResueltas').dataTable({
            ordering: false
        });

        $('#myTablePendientes').dataTable({
            ordering: false//,
            //"scrollY": "200px",
            //"scrollCollapse": true,
            //"paging": false
        });
    });


    //$(document).ready(function () {
    //    $("#population_chart").jChart({ x_label: "Population" });
    //    $("#colors_chart").jChart();
    //});


    $(document).ready(function () {

        // Estado
        if (document.getElementById("dbadm1_chartdiv2") == null) {
        }
        else {

            $.getJSON("/SS/CargarDBAdmTipoSolicitud", {},
                function (concData) {

                    makeChartsDBSolicitudesPorTipo(concData, 'light', '#ffffff');

                });
        }


        // Estado
        if (document.getElementById("db_chartdiv") == null) {
        }
        else {

            $.getJSON("/SS/CargarDBAdmTipoSolicitud", {},
                function (concData) {

                    makeChartsDBAdm(concData, 'light', '#ffffff');

                });
        }


        if (document.getElementById("db_chartdiv_categoria") == null) {
        }
        else {

            $.getJSON("/SS/CargarDBCategoria", {},
                function (concData2) {

                    makeChartsDBCategoria(concData2, 'light', '#ffffff');

                });
        }


        // Desplegar gráfico stack de estados de solicitudes por responsable 

        if (document.getElementById("db_chartdiv_estado_solicitud_por_responsable") == null) {
        }
        else {

            var aux = 0;

            //$.getJSON("/SS/CargarDBEstadoSolicitudPorResponsable", {},
            //    function (concData3) {

            //        makeChartsDBEstadoSolicitudPorResponsable(concData3);

            //    });

            $.ajax({
                type: 'GET',
                url: "/SS/CargarDBEstadoSolicitudPorResponsable",
                dataType: 'json',
                async: true,
                //data: myData,
                success: function (concData3) {
                    makeChartsDBEstadoSolicitudPorResponsable(concData3);
                }
            });
        }



        // Desplegar gráfico Barras por solicitudes pendientes atrasadas por responsable

        if (document.getElementById("db_chartdiv_solicitudes_pendientes_atrasadas_por_responsable") == null) {
        }
        else {

            $.getJSON("/SS/CargarDBSolicitudesPendientesAtrasadasPorResponsable", {},
                function (concData4) {

                    makeChartsDBSolicitudesPendientesAtrasadasPorResponsable(concData4, 'light');

                });
        }



    });




    if (document.getElementById("crearsss_descripcionid") == null) {
    }
    else {
        $("#crearsss_descripcionid").keyup(function (e) {
            var $this = $(this),
                charLength = $this.val().length,
                charLimit = $this.attr("maxlength");
            // Displays count
            document.getElementById("sss_caracteres").innerHTML = charLength + " de " + charLimit + " caracteres usados";
            //document.getElementById('sss_caracteres').html(charLength + " of " + charLimit + " characters used");
            // Alert when max is reached
            if ($this.val().length > charLimit) {
                document.getElementById("sss_caracteres").innerHTML = "<strong>Puede incorporar hasta " + charLimit + " caracteres.</strong>";
            }
        });

        $("#crearsss_descripcionid").keydown(function (e) {
            var $this = $(this),
                charLength = $this.val().length,
                charLimit = $this.attr("maxlength");

            if ($this.val().length > charLimit && e.keyCode !== 8 && e.keyCode !== 46) {
                return false;
            }
        });
    }


    if (document.getElementById("modificarsss_descripcionid") == null) {
    }
    else {

        var total_caractares_sss = $("#modificarsss_descripcionid").text().length;

        if (total_caractares_sss > 0)
            document.getElementById("sss_caracteres").innerHTML = total_caractares_sss + " de " + 350 + " caracteres usados";

        $("#modificarsss_descripcionid").keyup(function (e) {
            var $this = $(this),
                charLength_m = $this.val().length,
                charLimit_m = $this.attr("maxlength");
            // Displays count
            document.getElementById("sss_caracteres").innerHTML = charLength_m + " de " + charLimit_m + " caracteres usados";
            //document.getElementById('sss_caracteres').html(charLength + " of " + charLimit + " characters used");
            // Alert when max is reached
            if ($this.val().length > charLimit_m) {
                document.getElementById("sss_caracteres").innerHTML = "<strong>Puede incorporar hasta " + charLimit_m + " caracteres.</strong>";
            }
        });

        $("#modificarsss_descripcionid").keydown(function (e) {
            var $this = $(this),
                charLength_m = $this.val().length,
                charLimit_m = $this.attr("maxlength");

            if ($this.val().length > charLimit_m && e.keyCode !== 8 && e.keyCode !== 46) {
                return false;
            }
        });
    }


    if (document.getElementById("crearlogsss_descripcionid") == null) {
    }
    else {
        $("#crearlogsss_descripcionid").keyup(function (e) {
            var $this = $(this),
                charLength = $this.val().length,
                charLimit = $this.attr("maxlength");
            // Displays count
            document.getElementById("logsss_caracteres").innerHTML = charLength + " de " + charLimit + " caracteres usados";
            //document.getElementById('sss_caracteres').html(charLength + " of " + charLimit + " characters used");
            // Alert when max is reached
            if ($this.val().length > charLimit) {
                document.getElementById("logsss_caracteres").innerHTML = "<strong>Puede incorporar hasta " + charLimit + " caracteres.</strong>";
            }
        });

        $("#crearlogsss_descripcionid").keydown(function (e) {
            var $this = $(this),
                charLength = $this.val().length,
                charLimit = $this.attr("maxlength");

            if ($this.val().length > charLimit && e.keyCode !== 8 && e.keyCode !== 46) {
                return false;
            }
        });
    }






});















// Validar campos del formulario

function validateName(x) {
    // Validation rule
    var re = /[A-Za-z -']$/;
    // Check input
    if (re.test(x)) {
        // Style green
        //document.getElementById(x).style.background = '#ccffcc';  // fondo verde, si esta correcto
        // Hide error prompt
        //document.getElementById(x + '_error1').style.display = "none";
        //document.getElementById(x + '_error2').style.display = "none";
        return true;
    } else {
        // Style red
        //document.getElementById(x).style.background = '#e35152';
        // Show error prompt
        //document.getElementById(x + '_error2').style.display = "block";
        return false;
    }
}
// Validate email
function validateEmail(email) {
    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    if (re.test(email)) {
        //document.getElementById(elemento).style.background = '#ff0000'; // fondo rojo, si esta correcto el email
        //document.getElementById(elemento + '_error1').style.display = "none";
        //document.getElementById(elemento + '_error2').style.display = "none";
        return true;
    } else {
        //document.getElementById(elemento).style.background = '#0000ff'; // fondo azul, si es error
        return false;
    }
}
// Validate Select boxes
function validateSelect(x) {
    if (document.getElementById(x).selectedIndex !== 0) {
        document.getElementById(x).style.background = '#ccffcc';
        document.getElementById(x + 'Error').style.display = "none";
        return true;
    } else {
        document.getElementById(x).style.background = '#e35152';
        return false;
    }
}
function validateRadio(x) {
    if (document.getElementById(x).checked) {
        return true;
    } else {
        return false;
    }
}
function validateCheckbox(x) {
    if (document.getElementById(x).checked) {
        return true;
    }
    return false;
}


function validateForm() {
    // Set error catcher
    var error = 0;
    // Check name
    if (!validateName('name')) {
        document.getElementById('nameError').style.display = "block";
        error++;
    }
    // Validate email
    if (!validateEmail(document.getElementById('email').value)) {
        document.getElementById('emailError').style.display = "block";
        error++;
    }
    // Validate animal dropdown box
    if (!validateSelect('animal')) {
        document.getElementById('animalError').style.display = "block";
        error++;
    }
    // Validate Radio
    if (validateRadio('left')) {

    } else if (validateRadio('right')) {

    } else {
        document.getElementById('handError').style.display = "block";
        error++;
    }
    if (!validateCheckbox('accept')) {
        document.getElementById('termsError').style.display = "block";
        error++;
    }
    // Don't submit form if there are errors
    if (error > 0) {
        return false;
    }
}


// crearSSS - validar campos del formulario
function validateFormCrearSSS() {

    // Set error catcher
    var error = 0;


    if (document.getElementById("solicitanteemailid") == null) {
    }
    else {
        if (document.getElementById('solicitanteemailid').value.length == 0) {
            document.getElementById('solicitanteemailid_error2').style.display = "none";

            document.getElementById('solicitanteemailid_error1').style.display = "block";
            document.getElementById('solicitanteemailid_error1').style.color = '#ff3300';
            error++;
        }
        else {
            document.getElementById('solicitanteemailid_error1').style.display = "none";

            if (!validateEmail(document.getElementById('solicitanteemailid').value)) {
                document.getElementById('solicitanteemailid_error2').style.display = "block";
                document.getElementById('solicitanteemailid_error2').style.color = '#ff3300';
                error++;
            }
            else {
                document.getElementById('solicitanteemailid_error2').style.display = "none";
            }
        }
    }


    // campo opcional, entonces valida SOLO si se agrego algo
    if (document.getElementById("asignadoemailid") == null) {
    }
    else {
        if (document.getElementById('asignadoemailid').value.length == 0) {
            document.getElementById('asignadoemailid_error').style.display = "none";
        }
        else {
            // Validate email
            if (!validateEmail(document.getElementById('asignadoemailid').value)) {
                document.getElementById('asignadoemailid_error').style.display = "block";
                document.getElementById('asignadoemailid_error').style.color = '#ff3300';
                error++;
            }
            else {
                document.getElementById('asignadoemailid_error').style.display = "none";
            }
        }
    }


    //if (document.getElementById("crearsss_ubicacionid") == null) {
    //}
    //else {
    //    if (document.getElementById('crearsss_ubicacionid').value.length == 0) {
    //        document.getElementById('crearsss_ubicacionid_error2').style.display = "none";

    //        document.getElementById('crearsss_ubicacionid_error1').style.display = "block";
    //        document.getElementById('crearsss_ubicacionid_error1').style.color = '#ff3300';
    //        error++;
    //    }
    //    else {
    //        document.getElementById('crearsss_ubicacionid_error1').style.display = "none";
    //    }
    //}


    //if (document.getElementById("crearsss_tiposolicitud_ul") == null) {
    //}
    //else {
    //    if ($('#crearsss_tiposolicitud_ul li.active').length == 0) {
    //        document.getElementById('crearsss_tiposolicitud_ul_error').style.display = "block";
    //        document.getElementById('crearsss_tiposolicitud_ul_error').style.color = '#ff3300';
    //        error++;
    //    }
    //    else {
    //        document.getElementById('crearsss_tiposolicitud_ul_error').style.display = "none";
    //    }
    //}


    if (document.getElementById("crearsss_tiposolicitudid") == null) {
    }
    else {
        if (document.getElementById('crearsss_tiposolicitudid').value.length == 0) {
            document.getElementById('crearsss_tiposolicitudid_error').style.display = "block";
            document.getElementById('crearsss_tiposolicitudid_error').style.color = '#ff3300';
            error++;
        }
        else {
            document.getElementById('crearsss_tiposolicitudid_error').style.display = "none";
        }
    }



    //if (document.getElementById("crearsss_estado_ul") == null) {
    //}
    //else {
    //    if ($('#crearsss_estado_ul li.active').length == 0) {
    //        document.getElementById('crearsss_estado_ul_error').style.display = "block";
    //        document.getElementById('crearsss_estado_ul_error').style.color = '#ff3300';
    //        error++;
    //    }
    //    else {
    //        document.getElementById('crearsss_estado_ul_error').style.display = "none";
    //    }
    //}


    if (document.getElementById("crearsss_estadoid") == null) {
    }
    else {
        if (document.getElementById('crearsss_estadoid').value.length == 0) {
            document.getElementById('crearsss_estadoid_error').style.display = "block";
            document.getElementById('crearsss_estadoid_error').style.color = '#ff3300';
            error++;
        }
        else {
            document.getElementById('crearsss_estadoid_error').style.display = "none";
        }
    }


    if (document.getElementById("crearsss_descripcionid") == null) {
    }
    else {
        if (document.getElementById('crearsss_descripcionid').value.length == 0) {
            document.getElementById('crearsss_descripcionid_error').style.display = "block";
            document.getElementById('crearsss_descripcionid_error').style.color = '#ff3300';
            error++;
        }
        else {
            document.getElementById('crearsss_descripcionid_error').style.display = "none";
        }
    }

    // Don't submit form if there are errors
    if (error > 0) {
        return false;
    }

}

// crearSSSComp - validar campos del formulario
function validateFormCrearSSSComp() {

    // Set error catcher
    var error = 0;


    if (document.getElementById("solicitanteemailid") == null) {
    }
    else {
        if (document.getElementById('solicitanteemailid').value.length == 0) {
            document.getElementById('solicitanteemailid_error2').style.display = "none";

            document.getElementById('solicitanteemailid_error1').style.display = "block";
            document.getElementById('solicitanteemailid_error1').style.color = '#ff3300';
            error++;
        }
        else {
            document.getElementById('solicitanteemailid_error1').style.display = "none";

            if (!validateEmail(document.getElementById('solicitanteemailid').value)) {
                document.getElementById('solicitanteemailid_error2').style.display = "block";
                document.getElementById('solicitanteemailid_error2').style.color = '#ff3300';
                error++;
            }
            else {
                document.getElementById('solicitanteemailid_error2').style.display = "none";
            }
        }
    }


    // campo opcional, entonces valida SOLO si se agrego algo
    if (document.getElementById("asignadoemailid") == null) {
    }
    else {
        if (document.getElementById('asignadoemailid').value.length == 0) {
            document.getElementById('asignadoemailid_error').style.display = "none";
        }
        else {
            // Validate email
            if (!validateEmail(document.getElementById('asignadoemailid').value)) {
                document.getElementById('asignadoemailid_error').style.display = "block";
                document.getElementById('asignadoemailid_error').style.color = '#ff3300';
                error++;
            }
            else {
                document.getElementById('asignadoemailid_error').style.display = "none";
            }
        }
    }


    //if (document.getElementById("crearsss_ubicacionid") == null) {
    //}
    //else {
    //    if (document.getElementById('crearsss_ubicacionid').value.length == 0) {
    //        document.getElementById('crearsss_ubicacionid_error2').style.display = "none";

    //        document.getElementById('crearsss_ubicacionid_error1').style.display = "block";
    //        document.getElementById('crearsss_ubicacionid_error1').style.color = '#ff3300';
    //        error++;
    //    }
    //    else {
    //        document.getElementById('crearsss_ubicacionid_error1').style.display = "none";

    //        if (!validateName(document.getElementById('crearsss_ubicacionid').value)) {
    //            document.getElementById('crearsss_ubicacionid_error2').style.display = "block";
    //            document.getElementById('crearsss_ubicacionid_error2').style.color = '#ff3300';
    //            error++;
    //        }
    //        else {
    //            document.getElementById('crearsss_ubicacionid_error2').style.display = "none";
    //        }
    //    }
    //}


    if (document.getElementById("crearsss_tiposolicitudid") == null) {
    }
    else {
        if (document.getElementById('crearsss_tiposolicitudid').value.length == 0) {
            document.getElementById('crearsss_tiposolicitudid_error').style.display = "block";
            document.getElementById('crearsss_tiposolicitudid_error').style.color = '#ff3300';
            error++;
        }
        else {
            document.getElementById('crearsss_tiposolicitudid_error').style.display = "none";
        }
    }


    if (document.getElementById("crearssscomp_estadoid") == null) {
    }
    else {
        if (document.getElementById('crearssscomp_estadoid').value.length == 0) {
            document.getElementById('crearssscomp_estadoid_error').style.display = "block";
            document.getElementById('crearssscomp_estadoid_error').style.color = '#ff3300';
            error++;
        }
        else {
            document.getElementById('crearssscomp_estadoid_error').style.display = "none";
        }
    }


    if (document.getElementById("crearsss_descripcionid") == null) {
    }
    else {
        if (document.getElementById('crearsss_descripcionid').value.length == 0) {
            document.getElementById('crearsss_descripcionid_error').style.display = "block";
            document.getElementById('crearsss_descripcionid_error').style.color = '#ff3300';
            error++;
        }
        else {
            document.getElementById('crearsss_descripcionid_error').style.display = "none";
        }
    }



    // nuevo para creacion completa

    if (document.getElementById("crearsss_empresaid") == null) {
    }
    else {
        if (document.getElementById('crearsss_empresaid').value.length == 0) {
            document.getElementById('crearsss_empresaid_error').style.display = "block";
            document.getElementById('crearsss_empresaid_error').style.color = '#ff3300';
            error++;
        }
        else {
            document.getElementById('crearsss_empresaid_error').style.display = "none";
        }
    }

    //if (document.getElementById("crearsss_telefonoid") == null) {
    //}
    //else {
    //    if (document.getElementById('crearsss_telefonoid').value.length == 0) {
    //        document.getElementById('crearsss_telefonoid_error').style.display = "block";
    //        document.getElementById('crearsss_telefonoid_error').style.color = '#ff3300';
    //        error++;
    //    }
    //    else {
    //        document.getElementById('crearsss_telefonoid_error').style.display = "none";
    //    }
    //}

    if (document.getElementById("sss_categoriaid") == null) {
    }
    else {
        if (document.getElementById('sss_categoriaid').value.length == 0) {
            document.getElementById('sss_categoriaid_error').style.display = "block";
            document.getElementById('sss_categoriaid_error').style.color = '#ff3300';
            error++;
        }
        else {
            document.getElementById('sss_categoriaid_error').style.display = "none";
        }
    }


    if (document.getElementById("crearsss_horasid") == null) {
    }
    else {
        if (document.getElementById('crearsss_horasid').value.length == 0) {
            document.getElementById('crearsss_horasid_error').style.display = "block";
            document.getElementById('crearsss_horasid_error').style.color = '#ff3300';
            error++;
        }
        else {
            document.getElementById('crearsss_horasid_error').style.display = "none";
        }
    }

    if (document.getElementById("crearsss_fechasolicitadaid") == null) {
    }
    else {
        if (document.getElementById('crearsss_fechasolicitadaid').value.length == 0) {
            document.getElementById('crearsss_fechasolicitadaid_error').style.display = "block";
            document.getElementById('crearsss_fechasolicitadaid_error').style.color = '#ff3300';
            error++;
        }
        else {
            document.getElementById('crearsss_fechasolicitadaid_error').style.display = "none";
        }
    }

    if (document.getElementById("crearsss_fechacomprometidaid") == null) {
    }
    else {
        if (document.getElementById('crearsss_fechacomprometidaid').value.length == 0) {
            document.getElementById('crearsss_fechacomprometidaid_error').style.display = "block";
            document.getElementById('crearsss_fechacomprometidaid_error').style.color = '#ff3300';
            error++;
        }
        else {
            document.getElementById('crearsss_fechacomprometidaid_error').style.display = "none";
        }
    }


    //if (document.getElementById("crearsss_ccypid") == null) {
    //}
    //else {
    //    if (document.getElementById('crearsss_ccypid').value.length == 0) {
    //        document.getElementById('crearsss_ccypid_error').style.display = "block";
    //        document.getElementById('crearsss_ccypid_error').style.color = '#ff3300';
    //        error++;
    //    }
    //    else {
    //        document.getElementById('crearsss_ccypid_error').style.display = "none";
    //    }
    //}


    //if (document.getElementById("crearsss_sociedadid") == null) {
    //}
    //else {
    //    if (document.getElementById('crearsss_sociedadid').value.length == 0) {
    //        document.getElementById('crearsss_sociedadid_error').style.display = "block";
    //        document.getElementById('crearsss_sociedadid_error').style.color = '#ff3300';
    //        error++;
    //    }
    //    else {
    //        document.getElementById('crearsss_sociedadid_error').style.display = "none";
    //    }
    //}

    // Don't submit form if there are errors
    if (error > 0) {
        return false;
    }

}


// ModificarSSS - validar campos del formulario
function validateFormModificarSSS() {

    // Set error catcher
    var error = 0;


    if (document.getElementById("solicitanteemailid") == null) {
    }
    else {
        if (document.getElementById('solicitanteemailid').value.length == 0) {
            document.getElementById('solicitanteemailid_error2').style.display = "none";

            document.getElementById('solicitanteemailid_error1').style.display = "block";
            document.getElementById('solicitanteemailid_error1').style.color = '#ff3300';
            error++;
        }
        else {
            document.getElementById('solicitanteemailid_error1').style.display = "none";

            if (!validateEmail(document.getElementById('solicitanteemailid').value)) {
                document.getElementById('solicitanteemailid_error2').style.display = "block";
                document.getElementById('solicitanteemailid_error2').style.color = '#ff3300';
                error++;
            }
            else {
                document.getElementById('solicitanteemailid_error2').style.display = "none";
            }
        }
    }


    // campo opcional, entonces valida SOLO si se agrego algo
    if (document.getElementById("asignadoemailid") == null) {
    }
    else {
        if (document.getElementById('asignadoemailid').value.length == 0) {
            document.getElementById('asignadoemailid_error').style.display = "none";
        }
        else {
            // Validate email
            if (!validateEmail(document.getElementById('asignadoemailid').value)) {
                document.getElementById('asignadoemailid_error').style.display = "block";
                document.getElementById('asignadoemailid_error').style.color = '#ff3300';
                error++;
            }
            else {
                document.getElementById('asignadoemailid_error').style.display = "none";
            }
        }
    }


    //if (document.getElementById("modificarsss_ubicacionid") == null) {
    //}
    //else {
    //    if (document.getElementById('modificarsss_ubicacionid').value.length == 0) {
    //        document.getElementById('modificarsss_ubicacionid_error2').style.display = "none";

    //        document.getElementById('modificarsss_ubicacionid_error1').style.display = "block";
    //        document.getElementById('modificarsss_ubicacionid_error1').style.color = '#ff3300';
    //        error++;
    //    }
    //    else {
    //        document.getElementById('modificarsss_ubicacionid_error1').style.display = "none";
    //    }
    //}


    if (document.getElementById("modificarsss_tiposolicitudid") == null) {
    }
    else {
        if (document.getElementById('modificarsss_tiposolicitudid').value.length == 0) {
            document.getElementById('modificarsss_tiposolicitudid_error').style.display = "block";
            document.getElementById('modificarsss_tiposolicitudid_error').style.color = '#ff3300';
            error++;
        }
        else {
            document.getElementById('modificarsss_tiposolicitudid_error').style.display = "none";
        }
    }


    if (document.getElementById("modificarssscomp_estadoid") == null) {
    }
    else {
        if (document.getElementById('modificarssscomp_estadoid').value.length == 0) {
            document.getElementById('modificarssscomp_estadoid_error').style.display = "block";
            document.getElementById('modificarssscomp_estadoid_error').style.color = '#ff3300';
            error++;
        }
        else {
            document.getElementById('modificarssscomp_estadoid_error').style.display = "none";
        }
    }


    if (document.getElementById("modificarsss_descripcionid") == null) {
    }
    else {
        if (document.getElementById('modificarsss_descripcionid').value.length == 0) {
            document.getElementById('modificarsss_descripcionid_error').style.display = "block";
            document.getElementById('modificarsss_descripcionid_error').style.color = '#ff3300';
            error++;
        }
        else {
            document.getElementById('modificarsss_descripcionid_error').style.display = "none";
        }
    }



    // nuevo para creacion completa

    if (document.getElementById("modificarsss_empresaid") == null) {
    }
    else {
        if (document.getElementById('modificarsss_empresaid').value.length == 0) {
            document.getElementById('modificarsss_empresaid_error').style.display = "block";
            document.getElementById('modificarsss_empresaid_error').style.color = '#ff3300';
            error++;
        }
        else {
            document.getElementById('modificarsss_empresaid_error').style.display = "none";
        }
    }

    //if (document.getElementById("modificarsss_telefonoid") == null) {
    //}
    //else {
    //    if (document.getElementById('modificarsss_telefonoid').value.length == 0) {
    //        document.getElementById('modificarsss_telefonoid_error').style.display = "block";
    //        document.getElementById('modificarsss_telefonoid_error').style.color = '#ff3300';
    //        error++;
    //    }
    //    else {
    //        document.getElementById('modificarsss_telefonoid_error').style.display = "none";
    //    }
    //}

    if (document.getElementById("sss_categoriaid") == null) {
    }
    else {
        if (document.getElementById('sss_categoriaid').value.length == 0) {
            document.getElementById('sss_categoriaid_error').style.display = "block";
            document.getElementById('sss_categoriaid_error').style.color = '#ff3300';
            error++;
        }
        else {
            document.getElementById('sss_categoriaid_error').style.display = "none";
        }
    }



    if (document.getElementById("modificarsss_fechasolicitadaid") == null) {
    }
    else {
        if (document.getElementById('modificarsss_fechasolicitadaid').value.length == 0) {
            document.getElementById('modificarsss_fechasolicitadaid_error').style.display = "block";
            document.getElementById('modificarsss_fechasolicitadaid_error').style.color = '#ff3300';
            error++;
        }
        else {
            document.getElementById('modificarsss_fechasolicitadaid_error').style.display = "none";
        }
    }

    if (document.getElementById("modificarsss_fechacomprometidaid") == null) {
    }
    else {
        if (document.getElementById('modificarsss_fechacomprometidaid').value.length == 0) {
            document.getElementById('modificarsss_fechacomprometidaid_error').style.display = "block";
            document.getElementById('modificarsss_fechacomprometidaid_error').style.color = '#ff3300';
            error++;
        }
        else {
            document.getElementById('modificarsss_fechacomprometidaid_error').style.display = "none";
        }
    }


    // Don't submit form if there are errors
    if (error > 0) {
        return false;
    }

}

// crearSSS - validar campos del formulario
function validateFormFormularioContacto() {

    // Set error catcher
    var error = 0;

    if (document.getElementById("formulariocontactomensajeid") == null) {
    }
    else {
        if (document.getElementById('formulariocontactomensajeid').value.length == 0) {
            document.getElementById('formulariocontactomensajeid_error').style.display = "block";
            document.getElementById('formulariocontactomensajeid_error').style.color = '#ff3300';
            error++;
        }
        else {
            document.getElementById('formulariocontactomensajeid_error').style.display = "none";
        }
    }

    // Don't submit form if there are errors
    if (error > 0) {
        return false;
    }
}


// crearLogSSS - validar campos del formulario
function validateFormCrearLogSSS() {

    // Set error catcher
    var error = 0;

    if (document.getElementById("crearlogsss_horasid") == null) {
    }
    else {
        if (document.getElementById('crearlogsss_horasid').value.length == 0) {
            document.getElementById('crearlogsss_horasid_error').style.display = "block";
            document.getElementById('crearlogsss_horasid_error').style.color = '#ff3300';
            error++;
        }
        else {
            document.getElementById('crearlogsss_horasid_error').style.display = "none";
        }
    }

    if (document.getElementById("crearlogsss_descripcionid") == null) {
    }
    else {
        if (document.getElementById('crearlogsss_descripcionid').value.length == 0) {
            document.getElementById('crearlogsss_descripcionid_error').style.display = "block";
            document.getElementById('crearlogsss_descripcionid_error').style.color = '#ff3300';
            error++;
        }
        else {
            document.getElementById('crearlogsss_descripcionid_error').style.display = "none";
        }
    }

    // Don't submit form if there are errors
    if (error > 0) {
        return false;
    }

}


// Dashboard
// original
function makeChartsByClick(theme, bgColor, bgImage) {

    var chart1;
    var chart2;

    if (chart1) {
        chart1.clear();
    }
    if (chart2) {
        chart2.clear();
    }

    // background
    if (document.body) {
        document.body.style.backgroundColor = bgColor;
        document.body.style.backgroundImage = "url(" + bgImage + ")";
    }

    // column chart
    chart1 = AmCharts.makeChart("chartdiv1", {
        type: "serial",
        theme: theme,
        dataProvider: [{
            "year": 2005,
            "income": 23.5,
            "expenses": 18.1
        }, {
            "year": 2006,
            "income": 26.2,
            "expenses": 22.8
        }, {
            "year": 2007,
            "income": 30.1,
            "expenses": 23.9
        }, {
            "year": 2008,
            "income": 29.5,
            "expenses": 25.1
        }, {
            "year": 2009,
            "income": 24.6,
            "expenses": 25
        }],
        categoryField: "year",
        startDuration: 1,

        categoryAxis: {
            gridPosition: "start"
        },
        valueAxes: [{
            title: "Million USD"
        }],
        graphs: [{
            type: "column",
            title: "Income",
            valueField: "income",
            lineAlpha: 0,
            fillAlphas: 0.8,
            balloonText: "[[title]] in [[category]]:<b>[[value]]</b>"
        }, {
            type: "line",
            title: "Expenses",
            valueField: "expenses",
            lineThickness: 2,
            fillAlphas: 0,
            bullet: "round",
            balloonText: "[[title]] in [[category]]:<b>[[value]]</b>"
        }],
        legend: {
            useGraphSettings: true
        }

    });

    // pie chart
    chart2 = AmCharts.makeChart("chartdiv2", {
        type: "pie",
        theme: theme,
        dataProvider: [{
            "country": "Czech Republic",
            "litres": 156.9
        }, {
            "country": "Ireland",
            "litres": 131.1
        }, {
            "country": "Germany",
            "litres": 115.8
        }, {
            "country": "Australia",
            "litres": 109.9
        }, {
            "country": "Austria",
            "litres": 108.3
        }, {
            "country": "UK",
            "litres": 65
        }, {
            "country": "Belgium",
            "litres": 50
        }],
        titleField: "country",
        valueField: "litres",
        balloonText: "[[title]]<br><b>[[value]]</b> ([[percents]]%)",
        legend: {
            align: "center",
            markerType: "circle"
        }
    });
}

// Dashboard
// modificado por vsandoval para DashBoard de Administrador
function makeChartsDBSolicitudesPorTipo(data, theme, bgColor, bgImage) {

    var chart1;
    var chart2;



    var title = [];
    for (var i = 0; i < data.length; i++) {
        title[i] = {
            tipo: data[i].Text,
            cantidad: data[i].Value
        };
    }

    var rv = {};

    //var data_res = data.replace("\\", "");

    var data_res = [{
        "country": "Czech Republic",
        "litres": 156.9
    }, {
        "country": "Ireland",
        "litres": 131.1
    }, {
        "country": "Germany",
        "litres": 115.8
    }, {
        "country": "Australia",
        "litres": 109.9
    }, {
        "country": "Austria",
        "litres": 108.3
    }, {
        "country": "UK",
        "litres": 65
    }, {
        "country": "Belgium",
        "litres": 50
    }];

    if (chart1) {
        chart1.clear();
    }
    if (chart2) {
        chart2.clear();
    }

    //// background
    //if (document.body) {
    //    document.body.style.backgroundColor = bgColor;
    //    document.body.style.backgroundImage = "url(" + bgImage + ")";
    //}

    // column chart
    //chart1 = AmCharts.makeChart("dbadm_chartdiv1", {
    //    type: "serial",
    //    theme: theme,
    //    dataProvider: [{
    //        "year": 2005,
    //        "income": 23.5,
    //        "expenses": 18.1
    //    }, {
    //        "year": 2006,
    //        "income": 26.2,
    //        "expenses": 22.8
    //    }, {
    //        "year": 2007,
    //        "income": 30.1,
    //        "expenses": 23.9
    //    }, {
    //        "year": 2008,
    //        "income": 29.5,
    //        "expenses": 25.1
    //    }, {
    //        "year": 2009,
    //        "income": 24.6,
    //        "expenses": 25
    //    }],
    //    categoryField: "year",
    //    startDuration: 1,

    //    categoryAxis: {
    //        gridPosition: "start"
    //    },
    //    valueAxes: [{
    //        title: "Million USD"
    //    }],
    //    graphs: [{
    //        type: "column",
    //        title: "Income",
    //        valueField: "income",
    //        lineAlpha: 0,
    //        fillAlphas: 0.8,
    //        balloonText: "[[title]] in [[category]]:<b>[[value]]</b>"
    //    }, {
    //        type: "line",
    //        title: "Expenses",
    //        valueField: "expenses",
    //        lineThickness: 2,
    //        fillAlphas: 0,
    //        bullet: "round",
    //        balloonText: "[[title]] in [[category]]:<b>[[value]]</b>"
    //    }],
    //    legend: {
    //        useGraphSettings: true
    //    }

    //});

    // pie chart
    chart2 = AmCharts.makeChart("dbadm2_chartdiv2", {
        type: "pie",
        theme: theme,
        dataProvider: title,
        titleField: "tipo",
        valueField: "cantidad",
        balloonText: "[[title]]<br><b>[[value]]</b> ([[percents]]%)",
        legend: {
            align: "center",
            markerType: "circle"
        }
    });



}



function makeChartsDBAdm(data, theme, bgColor, bgImage) {

    var chart1;
    var chart2;



    var title = [];
    for (var i = 0; i < data.length; i++) {
        title[i] = {
            tipo: data[i].Text,
            cantidad: data[i].Value
        };
    }

    var rv = {};

    //var data_res = data.replace("\\", "");

    var data_res = [{
        "country": "Czech Republic",
        "litres": 156.9
    }, {
        "country": "Ireland",
        "litres": 131.1
    }, {
        "country": "Germany",
        "litres": 115.8
    }, {
        "country": "Australia",
        "litres": 109.9
    }, {
        "country": "Austria",
        "litres": 108.3
    }, {
        "country": "UK",
        "litres": 65
    }, {
        "country": "Belgium",
        "litres": 50
    }];

    if (chart1) {
        chart1.clear();
    }
    if (chart2) {
        chart2.clear();
    }

    //// background
    //if (document.body) {
    //    document.body.style.backgroundColor = bgColor;
    //    document.body.style.backgroundImage = "url(" + bgImage + ")";
    //}

    // column chart
    //chart1 = AmCharts.makeChart("dbadm_chartdiv1", {
    //    type: "serial",
    //    theme: theme,
    //    dataProvider: [{
    //        "year": 2005,
    //        "income": 23.5,
    //        "expenses": 18.1
    //    }, {
    //        "year": 2006,
    //        "income": 26.2,
    //        "expenses": 22.8
    //    }, {
    //        "year": 2007,
    //        "income": 30.1,
    //        "expenses": 23.9
    //    }, {
    //        "year": 2008,
    //        "income": 29.5,
    //        "expenses": 25.1
    //    }, {
    //        "year": 2009,
    //        "income": 24.6,
    //        "expenses": 25
    //    }],
    //    categoryField: "year",
    //    startDuration: 1,

    //    categoryAxis: {
    //        gridPosition: "start"
    //    },
    //    valueAxes: [{
    //        title: "Million USD"
    //    }],
    //    graphs: [{
    //        type: "column",
    //        title: "Income",
    //        valueField: "income",
    //        lineAlpha: 0,
    //        fillAlphas: 0.8,
    //        balloonText: "[[title]] in [[category]]:<b>[[value]]</b>"
    //    }, {
    //        type: "line",
    //        title: "Expenses",
    //        valueField: "expenses",
    //        lineThickness: 2,
    //        fillAlphas: 0,
    //        bullet: "round",
    //        balloonText: "[[title]] in [[category]]:<b>[[value]]</b>"
    //    }],
    //    legend: {
    //        useGraphSettings: true
    //    }

    //});

    // pie chart
    chart2 = AmCharts.makeChart("dbadm2_chartdiv2", {
        type: "pie",
        theme: theme,
        dataProvider: title,
        titleField: "tipo",
        valueField: "cantidad",
        balloonText: "[[title]]<br><b>[[value]]</b> ([[percents]]%)",
        legend: {
            align: "center",
            markerType: "circle"
        }
    });



}


function makeChartsDBCategoria(data, theme, bgColor, bgImage) {

    var chartCategoria;

    var title = [];
    for (var i = 0; i < data.length; i++) {
        title[i] = {
            categoria: data[i].Text,
            cantidad: data[i].Value
        };
    }

    if (chartCategoria) {
        chartCategoria.clear();
    }

    chartCategoria = AmCharts.makeChart("db_chartdiv1_categoria", {
        type: "radar",
        //dataProvider: [{
        //    "country": "Czech Republic",
        //    "litres": 156.9
        //}, {
        //    "country": "Ireland",
        //    "litres": 131.1
        //}, {
        //    "country": "UK",
        //    "litres": 99
        //}],

        dataProvider: title,

        categoryField: "categoria",
        startDuration: 2,


        valueAxes: [{
            axisAlpha: 0.15,
            minimum: 0,
            dashLength: 3,
            axisTitleOffset: 20,
            gridCount: 5
        }],

        graphs: [{

            valueField: "cantidad",

            bullet: "round",
            balloonText: "[[value]] solicitudes creadas para [[category]]"
        }],
        "export": {
            "enabled": true,
            "libs": {
                "autoLoad": false
            }
        }

    });


}


function makeChartsDBEstadoSolicitudPorResponsable(concData) {

    var chart;
    var auxiliar = concData;

    var chartData = [
        {
            "year": "2003",
            "europe": 2.5,
            "namerica": 2.5,
            "asia": 2.1,
            "lamerica": 0.3,
            "meast": 0.2,
            "africa": 0.1
        },
        {
            "year": "2004",
            "europe": 2.6,
            "namerica": 2.7,
            "asia": 2.2,
            "lamerica": 0.3,
            "meast": 0.3,
            "africa": 0.1
        },
        {
            "year": "2005",
            "europe": 2.8,
            "namerica": 2.9,
            "asia": 2.4,
            "lamerica": 0.3,
            "meast": 0.3,
            "africa": 0.1
        }
    ];


    // SERIAL CHART
    chart = new AmCharts.AmSerialChart();
    chart.dataProvider = concData;
    chart.categoryField = "NickName";
    chart.plotAreaBorderAlpha = 0.2;

    // AXES
    // category
    var categoryAxis = chart.categoryAxis;
    categoryAxis.gridAlpha = 0.1;
    categoryAxis.axisAlpha = 0;
    categoryAxis.gridPosition = "start";

    // value
    var valueAxis = new AmCharts.ValueAxis();
    valueAxis.stackType = "regular";
    valueAxis.gridAlpha = 0.1;
    valueAxis.axisAlpha = 0;
    chart.addValueAxis(valueAxis);

    // GRAPHS
    // first graph
    var graph = new AmCharts.AmGraph();
    graph.title = "Resuelta";
    graph.labelText = "[[value]]";
    graph.valueField = "Resuelta";
    graph.type = "column";
    graph.lineAlpha = 0;
    graph.fillAlphas = 1;
    graph.lineColor = "#B8B83F";
    graph.balloonText = "<span style='color:#555555;'>[[category]]</span><br><span style='font-size:14px'>[[title]]:<b>[[value]]</b></span>";
    chart.addGraph(graph);

    // 2do
    var graph = new AmCharts.AmGraph();
    graph.title = "Resuelta con Confirmación";
    graph.labelText = "[[value]]";
    graph.valueField = "ResueltaConConfirmacion";
    graph.type = "column";
    graph.lineAlpha = 0;
    graph.fillAlphas = 1;
    graph.lineColor = "#84B762";
    graph.balloonText = "<span style='color:#555555;'>[[category]]</span><br><span style='font-size:14px'>[[title]]:<b>[[value]]</b></span>";
    chart.addGraph(graph);

    // 3ero
    var graph = new AmCharts.AmGraph();
    graph.title = "No Corresponde a Tarea de Sistemas";
    graph.labelText = "[[value]]";
    graph.valueField = "NoCorrespondeATareaDeSistemas";
    graph.type = "column";
    graph.lineAlpha = 0;
    graph.fillAlphas = 1;
    graph.lineColor = "#D98C8C";
    graph.balloonText = "<span style='color:#555555;'>[[category]]</span><br><span style='font-size:14px'>[[title]]:<b>[[value]]</b></span>";
    chart.addGraph(graph);

    // 4to
    var graph = new AmCharts.AmGraph();
    graph.title = "Cierre Histórico";
    graph.labelText = "[[value]]";
    graph.valueField = "CierreHistorico";
    graph.type = "column";
    graph.lineAlpha = 0;
    graph.fillAlphas = 1;
    graph.lineColor = "#5B74BE";
    graph.balloonText = "<span style='color:#555555;'>[[category]]</span><br><span style='font-size:14px'>[[title]]:<b>[[value]]</b></span>";
    chart.addGraph(graph);

    // 5to
    var graph = new AmCharts.AmGraph();
    graph.title = "Esperando Respuesta de Usuario";
    graph.labelText = "[[value]]";
    graph.valueField = "EsperandoRespuestaDeUsuario";
    graph.type = "column";
    graph.lineAlpha = 0;
    graph.fillAlphas = 1;
    graph.lineColor = "#CD83AD";
    graph.balloonText = "<span style='color:#555555;'>[[category]]</span><br><span style='font-size:14px'>[[title]]:<b>[[value]]</b></span>";
    chart.addGraph(graph);

    // 6to
    var graph = new AmCharts.AmGraph();
    graph.title = "Escalada";
    graph.labelText = "[[value]]";
    graph.valueField = "Escalada";
    graph.type = "column";
    graph.lineAlpha = 0;
    graph.fillAlphas = 1;
    graph.lineColor = "#FFCC99";
    graph.balloonText = "<span style='color:#555555;'>[[category]]</span><br><span style='font-size:14px'>[[title]]:<b>[[value]]</b></span>";
    chart.addGraph(graph);

    // 7mo
    var graph = new AmCharts.AmGraph();
    graph.title = "Asignada";
    graph.labelText = "[[value]]";
    graph.valueField = "Asignada";
    graph.type = "column";
    graph.lineAlpha = 0;
    graph.fillAlphas = 1;
    graph.lineColor = "#cc99ff";
    graph.balloonText = "<span style='color:#555555;'>[[category]]</span><br><span style='font-size:14px'>[[title]]:<b>[[value]]</b></span>";
    chart.addGraph(graph);

    // 8vo
    var graph = new AmCharts.AmGraph();
    graph.title = "Agendada";
    graph.labelText = "[[value]]";
    graph.valueField = "Agendada";
    graph.type = "column";
    graph.lineAlpha = 0;
    graph.fillAlphas = 1;
    graph.lineColor = "#B8F9D9";
    graph.balloonText = "<span style='color:#555555;'>[[category]]</span><br><span style='font-size:14px'>[[title]]:<b>[[value]]</b></span>";
    chart.addGraph(graph);

    // 9no
    graph = new AmCharts.AmGraph();
    graph.title = "En Proceso";
    graph.labelText = "[[value]]";
    graph.valueField = "EnProceso";
    graph.type = "column";
    graph.lineAlpha = 0;
    graph.fillAlphas = 1;
    graph.lineColor = "#67B6DC";
    graph.balloonText = "<span style='color:#555555;'>[[category]]</span><br><span style='font-size:14px'>[[title]]:<b>[[value]]</b></span>";
    chart.addGraph(graph);

    // 10mo
    graph = new AmCharts.AmGraph();
    graph.title = "Nueva";
    graph.labelText = "[[value]]";
    graph.valueField = "Nueva";
    graph.type = "column";
    graph.lineAlpha = 0;
    graph.fillAlphas = 1;
    graph.lineColor = "#ffff99";
    graph.balloonText = "<span style='color:#555555;'>[[category]]</span><br><span style='font-size:14px'>[[title]]:<b>[[value]]</b></span>";
    chart.addGraph(graph);

    // LEGEND
    var legend = new AmCharts.AmLegend();
    legend.borderAlpha = 0.2;
    legend.horizontalGap = 10;
    chart.addLegend(legend);

    // WRITE
    chart.write("db_chartdiv1_estado_solicitud_por_responsable");


    // this method sets chart 2D/3D

}


function makeChartsDBSolicitudesPendientesAtrasadasPorResponsable(data, theme) {

    var chart1;
    var chart2;

    if (chart1) {
        chart1.clear();
    }
    if (chart2) {
        chart2.clear();
    }

    // column chart
    chart1 = AmCharts.makeChart("db_chartdiv1_solicitudes_pendientes_atrasadas_por_responsable", {
        type: "serial",
        theme: theme,
        dataProvider: data,
        categoryField: "NickName",
        startDuration: 1,

        categoryAxis: {
            gridPosition: "start"
        },
        valueAxes: [{
            title: "Solicitudes"
        }],
        graphs: [{
            type: "column",
            title: "Pendientes",
            valueField: "Pendientes",
            lineAlpha: 0,
            fillAlphas: 0.8,
            balloonText: "[[title]] por [[category]]:<b>[[value]]</b>"
        }, {
            type: "line",
            title: "Atrasadas",
            valueField: "Atrasadas",
            lineThickness: 2,
            fillAlphas: 0,
            bullet: "round",
            balloonText: "[[title]] por [[category]]:<b>[[value]]</b>"
        }],
        legend: {
            useGraphSettings: true
        }
    });

}





