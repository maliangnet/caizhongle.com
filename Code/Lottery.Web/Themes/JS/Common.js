var 
FormNew = 
    {
        Config:{
            //要求，1：必须有submit按钮，2：提示框下必须有span标签，3：模板内容必须赋值
            alert:"#divAlertNew",//模拟框
            modal:"#divNew",//模拟框内容
            form:"#formNew",//表单
            list:"#divList",//列表
            template:""//模板内容
        },
        Begin :function(){$(FormNew.Config.form).find("[type=submit]").button("loading");},
        Failure:function(xhr,status ,error){$(FormNew.Config.alert).removeClass("hide").find("span").html(error);$(FormNew.Config.form).find("[type=submit]").button("reset");},
        Sucess:function(json){
            $(FormNew.Config.form).find("[type=submit]").button("reset");
            if(!json.Success){
                $(FormNew.Config.alert).removeClass("hide").find("span").html(json.Message);
                return false;
            }
            else{
                $(FormNew.Config.form)[0].reset();
                var template = $(FormNew.Config.template);
                for(var attr in json.Model){
                    template.find("[data-name="+attr+"]").html(json.Model[attr]);
                }
                template.attr("data-key",json.Model.ID);
                $(FormNew.Config.list).find("tr:eq(0)").after(template);
                $(FormNew.Config.modal).modal("hide");
            }
            FormEvent.NewSucess(json);
        }
    },
FormEdit = 
    {
        Config:{
            //要求，1：必须有submit按钮，2：提示框下必须有span标签
            alert:"#divAlterEdit",//模拟框
            modal:"#divEdit",//模拟框内容
            form:"#formEdit",//表单
        },
        Begin:function(){$(FormEdit.Config.form).find("[type=submit]").button("loading");},
        Failure:function(xhr,status ,error){$(FormEdit.Config.alert).removeClass("hide").find("span").html(error);$(FormEdit.Config.form).find("[type=submit]").button("reset");},
        Sucess:function(json){
            $(FormEdit.Config.form).find("[type=submit]").button("reset");
            if(!json.Success){
                $(FormEdit.Config.alert).removeClass("hide").find("span").html(json.Message);
                return false;
            }
            else{
                $(FormEdit.Config.form)[0].reset();
                $(FormEdit.Config.modal).modal("hide");
                var tr = $("tr[data-key="+json.Model.ID+"]");
                for(var attr in json.Model){
                    tr.find("[data-name="+attr+"]").html(json.Model[attr]);
                }
            }
            FormEvent.EditSucess(json);
        }
    },
FormEvent =
    {        
        NewSucess:function(json){}, //新建成功后执行的事件
        EditSucess:function(json){}, //编辑成功后执行的事件
        Listen:function(json){} //编辑成功后执行的事件
    };
//页面加载
$(function(){
    //关闭模拟框事件
    $("div.alert").find("button").click(function(e){
        $(this).closest("div").addClass("hide");
        e.preventDefault();
    });
    //列表监听事件
    $(FormNew.Config.list).on("click","input","data",function(e){
        var data = $(this).attr("data-type");
        if(data=="edit"){ //编辑事件
            var tr = $(this).closest("tr");
            $(FormEdit.Config.modal).find("[data-name]").each(function(){
                var tagName = $(this)[0].tagName.toUpperCase();
                if(tagName == "INPUT" ||tagName == "SELECT")$(this).val(tr.find("[data-name="+$(this).attr("data-name")+"]").html());
                else $(this).html(tr.find("[data-name="+$(this).attr("data-name")+"]").html());                       
            });
            $(FormEdit.Config.modal).modal("show");
            FormEvent.Listen();
        }
    });  
})