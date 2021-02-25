{
	"patcher" : 	{
		"fileversion" : 1,
		"appversion" : 		{
			"major" : 8,
			"minor" : 1,
			"revision" : 8,
			"architecture" : "x64",
			"modernui" : 1
		}
,
		"classnamespace" : "box",
		"rect" : [ 1954.0, 72.0, 1468.0, 713.0 ],
		"bglocked" : 0,
		"openinpresentation" : 0,
		"default_fontsize" : 12.0,
		"default_fontface" : 0,
		"default_fontname" : "Arial",
		"gridonopen" : 1,
		"gridsize" : [ 15.0, 15.0 ],
		"gridsnaponopen" : 1,
		"objectsnaponopen" : 1,
		"statusbarvisible" : 2,
		"toolbarvisible" : 1,
		"lefttoolbarpinned" : 0,
		"toptoolbarpinned" : 0,
		"righttoolbarpinned" : 0,
		"bottomtoolbarpinned" : 0,
		"toolbars_unpinned_last_save" : 0,
		"tallnewobj" : 0,
		"boxanimatetime" : 200,
		"enablehscroll" : 1,
		"enablevscroll" : 1,
		"devicewidth" : 0.0,
		"description" : "",
		"digest" : "",
		"tags" : "",
		"style" : "",
		"subpatcher_template" : "",
		"assistshowspatchername" : 0,
		"boxes" : [ 			{
				"box" : 				{
					"id" : "obj-9",
					"linecount" : 12,
					"maxclass" : "message",
					"numinlets" : 2,
					"numoutlets" : 1,
					"outlettype" : [ "" ],
					"patching_rect" : [ 711.0, 562.0, 50.0, 174.0 ],
					"text" : "Romuald/poingDroit/ data/ 0.4 -0.26 -0.1 0.53 0.34 -0.09 0.81 0.66 0."
				}

			}
, 			{
				"box" : 				{
					"id" : "obj-4",
					"maxclass" : "newobj",
					"numinlets" : 1,
					"numoutlets" : 2,
					"outlettype" : [ "", "" ],
					"patching_rect" : [ 742.107138097286224, 510.964282691478729, 93.0, 22.0 ],
					"text" : "movuinoStream"
				}

			}
, 			{
				"box" : 				{
					"id" : "obj-8",
					"linecount" : 12,
					"maxclass" : "message",
					"numinlets" : 2,
					"numoutlets" : 1,
					"outlettype" : [ "" ],
					"patching_rect" : [ 192.0, 246.0, 50.0, 174.0 ],
					"text" : "Romuald/poingDroit/ 0.73 0.75 0.92 -0.66 0.73 -0.82 -0.22 -0.19 0.36"
				}

			}
, 			{
				"box" : 				{
					"id" : "obj-60",
					"maxclass" : "newobj",
					"numinlets" : 0,
					"numoutlets" : 1,
					"outlettype" : [ "" ],
					"patching_rect" : [ 1035.90908145904541, 131.363636016845703, 61.0, 22.0 ],
					"text" : "r rawData"
				}

			}
, 			{
				"box" : 				{
					"id" : "obj-55",
					"maxclass" : "comment",
					"numinlets" : 1,
					"numoutlets" : 0,
					"patching_rect" : [ 1035.90908145904541, 81.363636016845703, 236.0, 28.0 ],
					"style" : "textbutton001",
					"text" : "RAW DATAVIZ"
				}

			}
, 			{
				"box" : 				{
					"bgcolor" : [ 0.290196, 0.321569, 0.819608, 1.0 ],
					"candicane2" : [ 0.490196, 0.870588, 0.890196, 1.0 ],
					"candicane3" : [ 0.960784, 0.356863, 0.333333, 1.0 ],
					"candicane4" : [ 0.317647, 0.917647, 0.976471, 1.0 ],
					"candicane5" : [ 0.396078, 0.92549, 0.341176, 1.0 ],
					"candicane6" : [ 0.94902, 1.0, 0.443137, 1.0 ],
					"candycane" : 3,
					"contdata" : 1,
					"id" : "obj-6",
					"maxclass" : "multislider",
					"numinlets" : 1,
					"numoutlets" : 2,
					"outlettype" : [ "", "" ],
					"parameter_enable" : 0,
					"patching_rect" : [ 1035.90908145904541, 294.855100536346526, 354.0, 205.0 ],
					"setstyle" : 1,
					"size" : 9,
					"slidercolor" : [ 1.0, 0.94902, 0.478431, 1.0 ]
				}

			}
, 			{
				"box" : 				{
					"attr" : "size",
					"id" : "obj-49",
					"maxclass" : "attrui",
					"numinlets" : 1,
					"numoutlets" : 1,
					"outlettype" : [ "" ],
					"patching_rect" : [ 1035.90908145904541, 261.465270665969911, 150.0, 22.0 ]
				}

			}
, 			{
				"box" : 				{
					"id" : "obj-132",
					"maxclass" : "newobj",
					"numinlets" : 1,
					"numoutlets" : 0,
					"patching_rect" : [ 286.954296410083771, 260.268077157052176, 67.0, 22.0 ],
					"text" : "s startData"
				}

			}
, 			{
				"box" : 				{
					"id" : "obj-59",
					"maxclass" : "newobj",
					"numinlets" : 1,
					"numoutlets" : 0,
					"patching_rect" : [ 342.287629187107086, 334.977449841263933, 63.0, 22.0 ],
					"text" : "s rawData"
				}

			}
, 			{
				"box" : 				{
					"id" : "obj-931",
					"maxclass" : "message",
					"numinlets" : 2,
					"numoutlets" : 1,
					"outlettype" : [ "" ],
					"patching_rect" : [ 418.287629187107086, 176.557033143551962, 33.0, 22.0 ],
					"text" : "stop"
				}

			}
, 			{
				"box" : 				{
					"id" : "obj-1127",
					"maxclass" : "button",
					"numinlets" : 1,
					"numoutlets" : 1,
					"outlettype" : [ "bang" ],
					"parameter_enable" : 0,
					"patching_rect" : [ 342.287629187107086, 176.557033143551962, 24.0, 24.0 ]
				}

			}
, 			{
				"box" : 				{
					"id" : "obj-1128",
					"maxclass" : "newobj",
					"numinlets" : 2,
					"numoutlets" : 1,
					"outlettype" : [ "" ],
					"patcher" : 					{
						"fileversion" : 1,
						"appversion" : 						{
							"major" : 8,
							"minor" : 1,
							"revision" : 8,
							"architecture" : "x64",
							"modernui" : 1
						}
,
						"classnamespace" : "box",
						"rect" : [ 717.0, 215.0, 640.0, 480.0 ],
						"bglocked" : 0,
						"openinpresentation" : 0,
						"default_fontsize" : 12.0,
						"default_fontface" : 0,
						"default_fontname" : "Arial",
						"gridonopen" : 1,
						"gridsize" : [ 15.0, 15.0 ],
						"gridsnaponopen" : 1,
						"objectsnaponopen" : 1,
						"statusbarvisible" : 2,
						"toolbarvisible" : 1,
						"lefttoolbarpinned" : 0,
						"toptoolbarpinned" : 0,
						"righttoolbarpinned" : 0,
						"bottomtoolbarpinned" : 0,
						"toolbars_unpinned_last_save" : 0,
						"tallnewobj" : 0,
						"boxanimatetime" : 200,
						"enablehscroll" : 1,
						"enablevscroll" : 1,
						"devicewidth" : 0.0,
						"description" : "",
						"digest" : "",
						"tags" : "",
						"style" : "",
						"subpatcher_template" : "",
						"assistshowspatchername" : 0,
						"boxes" : [ 							{
								"box" : 								{
									"id" : "obj-12",
									"maxclass" : "newobj",
									"numinlets" : 1,
									"numoutlets" : 2,
									"outlettype" : [ "bang", "" ],
									"patching_rect" : [ 246.0, 319.0, 29.5, 22.0 ],
									"text" : "t b l"
								}

							}
, 							{
								"box" : 								{
									"id" : "obj-9",
									"maxclass" : "newobj",
									"numinlets" : 1,
									"numoutlets" : 1,
									"outlettype" : [ "bang" ],
									"patching_rect" : [ 83.0, 239.0, 58.0, 22.0 ],
									"text" : "loadbang"
								}

							}
, 							{
								"box" : 								{
									"id" : "obj-6",
									"maxclass" : "message",
									"numinlets" : 2,
									"numoutlets" : 1,
									"outlettype" : [ "" ],
									"patching_rect" : [ 83.0, 274.0, 118.0, 22.0 ],
									"text" : "Romuald/poingDroit/"
								}

							}
, 							{
								"box" : 								{
									"id" : "obj-3",
									"maxclass" : "newobj",
									"numinlets" : 2,
									"numoutlets" : 2,
									"outlettype" : [ "", "" ],
									"patching_rect" : [ 226.0, 356.0, 50.0, 22.0 ],
									"text" : "zl join"
								}

							}
, 							{
								"box" : 								{
									"id" : "obj-768",
									"maxclass" : "newobj",
									"numinlets" : 2,
									"numoutlets" : 1,
									"outlettype" : [ "int" ],
									"patching_rect" : [ 247.0, 181.0, 75.0, 22.0 ],
									"text" : "random 200"
								}

							}
, 							{
								"box" : 								{
									"id" : "obj-183",
									"linecount" : 3,
									"maxclass" : "comment",
									"numinlets" : 1,
									"numoutlets" : 0,
									"patching_rect" : [ 389.0, 250.298827999999958, 152.0, 48.0 ],
									"text" : "timer == longer after the zl group bc it takes 10 value (10 times longer)"
								}

							}
, 							{
								"box" : 								{
									"comment" : "",
									"id" : "obj-8",
									"index" : 2,
									"maxclass" : "inlet",
									"numinlets" : 0,
									"numoutlets" : 1,
									"outlettype" : [ "" ],
									"patching_rect" : [ 284.0, 74.0, 30.0, 30.0 ]
								}

							}
, 							{
								"box" : 								{
									"id" : "obj-7",
									"maxclass" : "newobj",
									"numinlets" : 2,
									"numoutlets" : 1,
									"outlettype" : [ "bang" ],
									"patching_rect" : [ 247.0, 151.0, 63.0, 22.0 ],
									"text" : "metro 100"
								}

							}
, 							{
								"box" : 								{
									"id" : "obj-4",
									"linecount" : 9,
									"maxclass" : "message",
									"numinlets" : 2,
									"numoutlets" : 1,
									"outlettype" : [ "" ],
									"patching_rect" : [ 471.0, 336.0, 44.0, 133.0 ],
									"text" : "0.73 0.75 0.92 -0.66 0.73 -0.82 -0.22 -0.19 0.36"
								}

							}
, 							{
								"box" : 								{
									"id" : "obj-931",
									"maxclass" : "message",
									"numinlets" : 2,
									"numoutlets" : 1,
									"outlettype" : [ "" ],
									"patching_rect" : [ 284.0, 122.0, 33.0, 22.0 ],
									"text" : "stop"
								}

							}
, 							{
								"box" : 								{
									"comment" : "",
									"id" : "obj-2",
									"index" : 1,
									"maxclass" : "inlet",
									"numinlets" : 0,
									"numoutlets" : 1,
									"outlettype" : [ "bang" ],
									"patching_rect" : [ 246.0, 74.0, 30.0, 30.0 ]
								}

							}
, 							{
								"box" : 								{
									"comment" : "",
									"id" : "obj-1",
									"index" : 1,
									"maxclass" : "outlet",
									"numinlets" : 1,
									"numoutlets" : 0,
									"patching_rect" : [ 226.0, 403.819277286529541, 30.0, 30.0 ]
								}

							}
, 							{
								"box" : 								{
									"id" : "obj-858",
									"maxclass" : "button",
									"numinlets" : 1,
									"numoutlets" : 1,
									"outlettype" : [ "bang" ],
									"parameter_enable" : 0,
									"patching_rect" : [ 246.0, 122.0, 24.0, 24.0 ]
								}

							}
, 							{
								"box" : 								{
									"id" : "obj-843",
									"maxclass" : "newobj",
									"numinlets" : 2,
									"numoutlets" : 1,
									"outlettype" : [ "float" ],
									"patching_rect" : [ 246.0, 244.199218999999999, 39.0, 22.0 ],
									"text" : "/ 100."
								}

							}
, 							{
								"box" : 								{
									"id" : "obj-841",
									"maxclass" : "newobj",
									"numinlets" : 2,
									"numoutlets" : 1,
									"outlettype" : [ "int" ],
									"patching_rect" : [ 246.0, 212.0, 38.0, 22.0 ],
									"text" : "- 100"
								}

							}
, 							{
								"box" : 								{
									"id" : "obj-799",
									"maxclass" : "newobj",
									"numinlets" : 2,
									"numoutlets" : 2,
									"outlettype" : [ "", "" ],
									"patching_rect" : [ 246.0, 276.298828000000015, 61.0, 22.0 ],
									"text" : "zl group 9"
								}

							}
 ],
						"lines" : [ 							{
								"patchline" : 								{
									"destination" : [ "obj-3", 1 ],
									"source" : [ "obj-12", 1 ]
								}

							}
, 							{
								"patchline" : 								{
									"destination" : [ "obj-3", 0 ],
									"midpoints" : [ 255.5, 342.0, 235.5, 342.0 ],
									"source" : [ "obj-12", 0 ]
								}

							}
, 							{
								"patchline" : 								{
									"destination" : [ "obj-858", 0 ],
									"source" : [ "obj-2", 0 ]
								}

							}
, 							{
								"patchline" : 								{
									"destination" : [ "obj-1", 0 ],
									"source" : [ "obj-3", 0 ]
								}

							}
, 							{
								"patchline" : 								{
									"destination" : [ "obj-3", 0 ],
									"midpoints" : [ 92.5, 321.0, 235.5, 321.0 ],
									"source" : [ "obj-6", 0 ]
								}

							}
, 							{
								"patchline" : 								{
									"destination" : [ "obj-768", 0 ],
									"source" : [ "obj-7", 0 ]
								}

							}
, 							{
								"patchline" : 								{
									"destination" : [ "obj-841", 0 ],
									"source" : [ "obj-768", 0 ]
								}

							}
, 							{
								"patchline" : 								{
									"destination" : [ "obj-12", 0 ],
									"order" : 1,
									"source" : [ "obj-799", 0 ]
								}

							}
, 							{
								"patchline" : 								{
									"destination" : [ "obj-4", 1 ],
									"midpoints" : [ 255.5, 311.0, 505.5, 311.0 ],
									"order" : 0,
									"source" : [ "obj-799", 0 ]
								}

							}
, 							{
								"patchline" : 								{
									"destination" : [ "obj-931", 0 ],
									"source" : [ "obj-8", 0 ]
								}

							}
, 							{
								"patchline" : 								{
									"destination" : [ "obj-843", 0 ],
									"source" : [ "obj-841", 0 ]
								}

							}
, 							{
								"patchline" : 								{
									"destination" : [ "obj-799", 0 ],
									"source" : [ "obj-843", 0 ]
								}

							}
, 							{
								"patchline" : 								{
									"destination" : [ "obj-7", 0 ],
									"source" : [ "obj-858", 0 ]
								}

							}
, 							{
								"patchline" : 								{
									"destination" : [ "obj-6", 0 ],
									"source" : [ "obj-9", 0 ]
								}

							}
, 							{
								"patchline" : 								{
									"destination" : [ "obj-7", 1 ],
									"source" : [ "obj-931", 0 ]
								}

							}
 ],
						"styles" : [ 							{
								"name" : "textbutton001",
								"default" : 								{
									"fontsize" : [ 18.0 ],
									"color" : [ 1.0, 1.0, 1.0, 1.0 ],
									"fontname" : [ "Museo Sans Rounded 300" ],
									"bgcolor" : [ 0.368627, 0.537255, 0.929412, 1.0 ],
									"accentcolor" : [ 0.92549, 0.364706, 0.341176, 0.0 ],
									"selectioncolor" : [ 0.815686, 0.858824, 0.34902, 0.0 ],
									"elementcolor" : [ 0.368627, 0.537255, 0.929412, 1.0 ]
								}
,
								"parentstyle" : "",
								"multi" : 0
							}
 ]
					}
,
					"patching_rect" : [ 342.287629187107086, 212.224026143550873, 95.0, 22.0 ],
					"saved_object_attributes" : 					{
						"description" : "",
						"digest" : "",
						"globalpatchername" : "",
						"tags" : ""
					}
,
					"text" : "p dataSimulator"
				}

			}
, 			{
				"box" : 				{
					"id" : "obj-3",
					"maxclass" : "newobj",
					"numinlets" : 1,
					"numoutlets" : 2,
					"outlettype" : [ "", "" ],
					"patching_rect" : [ 499.107138097286224, 316.964282691478729, 93.0, 22.0 ],
					"text" : "movuinoStream"
				}

			}
, 			{
				"box" : 				{
					"fontname" : "Arial",
					"fontsize" : 13.0,
					"id" : "obj-12",
					"maxclass" : "message",
					"numinlets" : 2,
					"numoutlets" : 1,
					"outlettype" : [ "" ],
					"patching_rect" : [ 499.107138097286224, 212.224026143550873, 65.0, 23.0 ],
					"text" : "port 7400"
				}

			}
, 			{
				"box" : 				{
					"fontname" : "Arial",
					"fontsize" : 13.0,
					"id" : "obj-19",
					"maxclass" : "message",
					"numinlets" : 2,
					"numoutlets" : 1,
					"outlettype" : [ "" ],
					"patching_rect" : [ 737.0, 399.855100893974395, 67.0, 23.0 ],
					"text" : "port 7401"
				}

			}
, 			{
				"box" : 				{
					"fontname" : "Arial",
					"fontsize" : 13.0,
					"id" : "obj-21",
					"maxclass" : "message",
					"numinlets" : 2,
					"numoutlets" : 1,
					"outlettype" : [ "" ],
					"patching_rect" : [ 711.0, 370.855100536346526, 93.0, 23.0 ],
					"text" : "host 127.0.0.1"
				}

			}
, 			{
				"box" : 				{
					"id" : "obj-2",
					"maxclass" : "newobj",
					"numinlets" : 1,
					"numoutlets" : 0,
					"patching_rect" : [ 499.107138097286224, 449.214282155036926, 138.0, 22.0 ],
					"text" : "udpsend 127.0.0.1 7401"
				}

			}
, 			{
				"box" : 				{
					"id" : "obj-1",
					"maxclass" : "newobj",
					"numinlets" : 1,
					"numoutlets" : 1,
					"outlettype" : [ "" ],
					"patching_rect" : [ 499.107138097286224, 252.750001013278961, 97.0, 22.0 ],
					"text" : "udpreceive 7400"
				}

			}
 ],
		"lines" : [ 			{
				"patchline" : 				{
					"destination" : [ "obj-1128", 0 ],
					"order" : 0,
					"source" : [ "obj-1127", 0 ]
				}

			}
, 			{
				"patchline" : 				{
					"destination" : [ "obj-132", 0 ],
					"midpoints" : [ 351.787629187107086, 206.678572118282318, 296.454296410083771, 206.678572118282318 ],
					"order" : 1,
					"source" : [ "obj-1127", 0 ]
				}

			}
, 			{
				"patchline" : 				{
					"destination" : [ "obj-3", 0 ],
					"order" : 0,
					"source" : [ "obj-1128", 0 ]
				}

			}
, 			{
				"patchline" : 				{
					"destination" : [ "obj-59", 0 ],
					"order" : 1,
					"source" : [ "obj-1128", 0 ]
				}

			}
, 			{
				"patchline" : 				{
					"destination" : [ "obj-8", 1 ],
					"order" : 2,
					"source" : [ "obj-1128", 0 ]
				}

			}
, 			{
				"patchline" : 				{
					"destination" : [ "obj-1", 0 ],
					"source" : [ "obj-12", 0 ]
				}

			}
, 			{
				"patchline" : 				{
					"destination" : [ "obj-2", 0 ],
					"midpoints" : [ 746.5, 436.0, 508.607138097286224, 436.0 ],
					"source" : [ "obj-19", 0 ]
				}

			}
, 			{
				"patchline" : 				{
					"destination" : [ "obj-2", 0 ],
					"midpoints" : [ 720.5, 436.0, 508.607138097286224, 436.0 ],
					"source" : [ "obj-21", 0 ]
				}

			}
, 			{
				"patchline" : 				{
					"destination" : [ "obj-9", 1 ],
					"source" : [ "obj-4", 0 ]
				}

			}
, 			{
				"patchline" : 				{
					"destination" : [ "obj-6", 0 ],
					"source" : [ "obj-49", 0 ]
				}

			}
, 			{
				"patchline" : 				{
					"destination" : [ "obj-6", 0 ],
					"source" : [ "obj-60", 0 ]
				}

			}
, 			{
				"patchline" : 				{
					"destination" : [ "obj-1128", 1 ],
					"source" : [ "obj-931", 0 ]
				}

			}
 ],
		"parameters" : 		{
			"obj-3::obj-633" : [ "live.text[5]", "live.text", 0 ],
			"obj-3::obj-636" : [ "live.text[6]", "live.text", 0 ],
			"obj-3::obj-638" : [ "live.text[7]", "live.text", 0 ],
			"obj-3::obj-761" : [ "live.text[2]", "live.text", 0 ],
			"obj-3::obj-775" : [ "live.text[3]", "live.text", 0 ],
			"obj-3::obj-781" : [ "live.text[4]", "live.text", 0 ],
			"obj-4::obj-633" : [ "live.text[12]", "live.text", 0 ],
			"obj-4::obj-636" : [ "live.text[8]", "live.text", 0 ],
			"obj-4::obj-638" : [ "live.text[9]", "live.text", 0 ],
			"obj-4::obj-761" : [ "live.text[10]", "live.text", 0 ],
			"obj-4::obj-775" : [ "live.text[13]", "live.text", 0 ],
			"obj-4::obj-781" : [ "live.text[11]", "live.text", 0 ],
			"parameterbanks" : 			{

			}
,
			"parameter_overrides" : 			{
				"obj-4::obj-633" : 				{
					"parameter_longname" : "live.text[12]"
				}
,
				"obj-4::obj-636" : 				{
					"parameter_longname" : "live.text[8]"
				}
,
				"obj-4::obj-638" : 				{
					"parameter_longname" : "live.text[9]"
				}
,
				"obj-4::obj-761" : 				{
					"parameter_longname" : "live.text[10]"
				}
,
				"obj-4::obj-775" : 				{
					"parameter_longname" : "live.text[13]"
				}
,
				"obj-4::obj-781" : 				{
					"parameter_longname" : "live.text[11]"
				}

			}
,
			"inherited_shortname" : 1
		}
,
		"dependency_cache" : [ 			{
				"name" : "movuinoStream.maxpat",
				"bootpath" : "~/Documents/GitHub/HTBX_SparringManager/PatchMax/patchers",
				"patcherrelativepath" : ".",
				"type" : "JSON",
				"implicit" : 1
			}
, 			{
				"name" : "MovingMean.js",
				"bootpath" : "~/Documents/GitHub/HTBX_SparringManager/PatchMax/code",
				"patcherrelativepath" : "../code",
				"type" : "TEXT",
				"implicit" : 1
			}
, 			{
				"name" : "integrateData.js",
				"bootpath" : "~/Documents/GitHub/HTBX_SparringManager/PatchMax/code",
				"patcherrelativepath" : "../code",
				"type" : "TEXT",
				"implicit" : 1
			}
, 			{
				"name" : "mubu.mxe64",
				"type" : "mx64"
			}
, 			{
				"name" : "mubu.hhmm.mxe64",
				"type" : "mx64"
			}
, 			{
				"name" : "mubu.mxe64",
				"type" : "mx64"
			}
 ],
		"autosave" : 0,
		"styles" : [ 			{
				"name" : "textbutton001",
				"default" : 				{
					"fontsize" : [ 18.0 ],
					"color" : [ 1.0, 1.0, 1.0, 1.0 ],
					"fontname" : [ "Museo Sans Rounded 300" ],
					"bgcolor" : [ 0.368627, 0.537255, 0.929412, 1.0 ],
					"accentcolor" : [ 0.92549, 0.364706, 0.341176, 0.0 ],
					"selectioncolor" : [ 0.815686, 0.858824, 0.34902, 0.0 ],
					"elementcolor" : [ 0.368627, 0.537255, 0.929412, 1.0 ]
				}
,
				"parentstyle" : "",
				"multi" : 0
			}
 ]
	}

}
