(window.webpackJsonp=window.webpackJsonp||[]).push([[119],{196:function(e,n,t){"use strict";t.r(n),t.d(n,"frontMatter",(function(){return i})),t.d(n,"metadata",(function(){return c})),t.d(n,"toc",(function(){return l})),t.d(n,"default",(function(){return b}));var r=t(3),o=t(7),a=(t(0),t(214)),i=(t(10),{slug:"expando-object",title:"4. \u7c98\u571f\u5bf9\u8c61",author:"dotNET China",author_title:"\u4e3a\u4e2d\u56fd .NET \u5f00\u53d1\u8005\u63d0\u4f9b\u4f18\u8d28\u7684\u8d44\u8baf\u548c\u6280\u672f\u5206\u4eab\u3002",author_url:"https://www.chinadot.net",author_image_url:"https://i.loli.net/2021/01/19/M8q5a3OTZWUKicl.png",tags:["furion","furos",".net",".netcore",".net5","clay","ExpandoObject","dictionary"]}),c={permalink:"/blog/expando-object",editUrl:"https://gitee.com/dotnetchina/Furion/tree/master/handbook/blog/2021-04-21-expando-object.mdx",source:"@site/blog/2021-04-21-expando-object.mdx",title:"4. \u7c98\u571f\u5bf9\u8c61",description:"\u5bf9\u8c61\u8f6c IDictionary",date:"2021-04-21T00:00:00.000Z",formattedDate:"April 21, 2021",tags:[{label:"furion",permalink:"/blog/tags/furion"},{label:"furos",permalink:"/blog/tags/furos"},{label:".net",permalink:"/blog/tags/net"},{label:".netcore",permalink:"/blog/tags/netcore"},{label:".net5",permalink:"/blog/tags/net-5"},{label:"clay",permalink:"/blog/tags/clay"},{label:"ExpandoObject",permalink:"/blog/tags/expando-object"},{label:"dictionary",permalink:"/blog/tags/dictionary"}],readingTime:.515,truncated:!1,nextItem:{title:"3. \u6587\u4ef6\u4e0a\u4f20\u4e0b\u8f7d",permalink:"/blog/fileupload-download"}},l=[{value:"\u5bf9\u8c61\u8f6c <code>IDictionary&lt;string, object&gt;</code>",id:"\u5bf9\u8c61\u8f6c-idictionarystring-object",children:[]},{value:"\u5bf9\u8c61\u8f6c <code>IDictionary&lt;string, Tuple&lt;Type, object&gt;&gt;</code>",id:"\u5bf9\u8c61\u8f6c-idictionarystring-tupletype-object",children:[]},{value:"\u5bf9\u8c61\u8f6c <code>ExpandoObject</code> \u7c7b\u578b",id:"\u5bf9\u8c61\u8f6c-expandoobject-\u7c7b\u578b",children:[]}],p={toc:l};function b(e){var n=e.components,t=Object(o.a)(e,["components"]);return Object(a.b)("wrapper",Object(r.a)({},p,t,{components:n,mdxType:"MDXLayout"}),Object(a.b)("h2",{id:"\u5bf9\u8c61\u8f6c-idictionarystring-object"},"\u5bf9\u8c61\u8f6c ",Object(a.b)("inlineCode",{parentName:"h2"},"IDictionary<string, object>")),Object(a.b)("p",null,"\u6709\u4e9b\u65f6\u5019\u6211\u4eec\u9700\u8981\u5c06\u4e00\u4e2a\u5bf9\u8c61\u6216\u533f\u540d\u7c7b\u8f6c\u6362\u6210\u5b57\u5178\u7c7b\u578b\uff0c\u5c31\u9700\u8981\u7528\u5230\u8be5\u529f\u80fd\u3002"),Object(a.b)("pre",null,Object(a.b)("code",{parentName:"pre",className:"language-cs",metastring:"{4}","{4}":!0},'var dic = new {\n    Id = 1,\n    Name = "Furion"\n}.ToDictionary();\n\nforeach (var key in dic)\n{\n    Console.WriteLine(dic[key]);\n}\n')),Object(a.b)("h2",{id:"\u5bf9\u8c61\u8f6c-idictionarystring-tupletype-object"},"\u5bf9\u8c61\u8f6c ",Object(a.b)("inlineCode",{parentName:"h2"},"IDictionary<string, Tuple<Type, object>>")),Object(a.b)("pre",null,Object(a.b)("code",{parentName:"pre",className:"language-cs",metastring:"{4}","{4}":!0},'var dic = new {\n    Id = 1,\n    Name = "Furion"\n}.ToDictionaryWithType();\n\nforeach (var key in dic)\n{\n    Console.WriteLine(dic[key].Item1);\n    Console.WriteLine(dic[key].Item2);\n}\n')),Object(a.b)("h2",{id:"\u5bf9\u8c61\u8f6c-expandoobject-\u7c7b\u578b"},"\u5bf9\u8c61\u8f6c ",Object(a.b)("inlineCode",{parentName:"h2"},"ExpandoObject")," \u7c7b\u578b"),Object(a.b)("pre",null,Object(a.b)("code",{parentName:"pre",className:"language-cs"},'dynamic expando = new {\n    Id = 1,\n    Name = "\u767e\u5c0f\u50e7",\n    Project = new {\n        Name = "Furion"\n    }\n}.ToExpandoObject();\n\n// \u52a8\u6001\u6dfb\u52a0\u5c5e\u6027\nexpando.NickName = "MonkSoul";\nexpando.Project.Id = 1;\n\nConsole.WriteLine(expando.Name);    // => \u767e\u5c0f\u50e7\nConsole.WriteLine(expando.Project.NickName);    // => MonkSoul\n')))}b.isMDXComponent=!0},214:function(e,n,t){"use strict";t.d(n,"a",(function(){return d})),t.d(n,"b",(function(){return m}));var r=t(0),o=t.n(r);function a(e,n,t){return n in e?Object.defineProperty(e,n,{value:t,enumerable:!0,configurable:!0,writable:!0}):e[n]=t,e}function i(e,n){var t=Object.keys(e);if(Object.getOwnPropertySymbols){var r=Object.getOwnPropertySymbols(e);n&&(r=r.filter((function(n){return Object.getOwnPropertyDescriptor(e,n).enumerable}))),t.push.apply(t,r)}return t}function c(e){for(var n=1;n<arguments.length;n++){var t=null!=arguments[n]?arguments[n]:{};n%2?i(Object(t),!0).forEach((function(n){a(e,n,t[n])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(t)):i(Object(t)).forEach((function(n){Object.defineProperty(e,n,Object.getOwnPropertyDescriptor(t,n))}))}return e}function l(e,n){if(null==e)return{};var t,r,o=function(e,n){if(null==e)return{};var t,r,o={},a=Object.keys(e);for(r=0;r<a.length;r++)t=a[r],n.indexOf(t)>=0||(o[t]=e[t]);return o}(e,n);if(Object.getOwnPropertySymbols){var a=Object.getOwnPropertySymbols(e);for(r=0;r<a.length;r++)t=a[r],n.indexOf(t)>=0||Object.prototype.propertyIsEnumerable.call(e,t)&&(o[t]=e[t])}return o}var p=o.a.createContext({}),b=function(e){var n=o.a.useContext(p),t=n;return e&&(t="function"==typeof e?e(n):c(c({},n),e)),t},d=function(e){var n=b(e.components);return o.a.createElement(p.Provider,{value:n},e.children)},u={inlineCode:"code",wrapper:function(e){var n=e.children;return o.a.createElement(o.a.Fragment,{},n)}},s=o.a.forwardRef((function(e,n){var t=e.components,r=e.mdxType,a=e.originalType,i=e.parentName,p=l(e,["components","mdxType","originalType","parentName"]),d=b(t),s=r,m=d["".concat(i,".").concat(s)]||d[s]||u[s]||a;return t?o.a.createElement(m,c(c({ref:n},p),{},{components:t})):o.a.createElement(m,c({ref:n},p))}));function m(e,n){var t=arguments,r=n&&n.mdxType;if("string"==typeof e||r){var a=t.length,i=new Array(a);i[0]=s;var c={};for(var l in n)hasOwnProperty.call(n,l)&&(c[l]=n[l]);c.originalType=e,c.mdxType="string"==typeof e?e:r,i[1]=c;for(var p=2;p<a;p++)i[p]=t[p];return o.a.createElement.apply(null,i)}return o.a.createElement.apply(null,t)}s.displayName="MDXCreateElement"}}]);