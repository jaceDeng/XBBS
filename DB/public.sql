/*
Navicat PGSQL Data Transfer

Source Server         : LotterySimulator
Source Server Version : 90112
Source Host           : www.olexe.cn:5432
Source Database       : JexusBBS
Source Schema         : public

Target Server Type    : PGSQL
Target Server Version : 90112
File Encoding         : 65001

Date: 2014-04-22 10:21:19
*/


-- ----------------------------
-- Sequence structure for jexus_comments_seq
-- ----------------------------
DROP SEQUENCE "public"."jexus_comments_seq";
CREATE SEQUENCE "public"."jexus_comments_seq"
 INCREMENT 1
 MINVALUE 1
 MAXVALUE 9223372036854775807
 START 12
 CACHE 1;
SELECT setval('"public"."jexus_comments_seq"', 12, true);

-- ----------------------------
-- Sequence structure for jexus_forums_fid_seq
-- ----------------------------
DROP SEQUENCE "public"."jexus_forums_fid_seq";
CREATE SEQUENCE "public"."jexus_forums_fid_seq"
 INCREMENT 1
 MINVALUE 1
 MAXVALUE 9223372036854775807
 START 11
 CACHE 1;
SELECT setval('"public"."jexus_forums_fid_seq"', 11, true);

-- ----------------------------
-- Sequence structure for jexus_users_uid_seq
-- ----------------------------
DROP SEQUENCE "public"."jexus_users_uid_seq";
CREATE SEQUENCE "public"."jexus_users_uid_seq"
 INCREMENT 1
 MINVALUE 1
 MAXVALUE 9223372036854775807
 START 12
 CACHE 1;
SELECT setval('"public"."jexus_users_uid_seq"', 12, true);

-- ----------------------------
-- Table structure for jexus_categories
-- ----------------------------
DROP TABLE IF EXISTS "public"."jexus_categories";
CREATE TABLE "public"."jexus_categories" (
"cid" int2 NOT NULL,
"pid" int2 NOT NULL,
"cname" varchar(30) COLLATE "default",
"content" varchar(255) COLLATE "default",
"keywords" varchar(255) COLLATE "default",
"ico" varchar(128) COLLATE "default",
"master" varchar(100) COLLATE "default" NOT NULL,
"permit" varchar(255) COLLATE "default",
"listnum" int4,
"clevel" varchar(25) COLLATE "default",
"cord" int2
)
WITH (OIDS=FALSE)

;
COMMENT ON COLUMN "public"."jexus_categories"."cname" IS '分类名称';

-- ----------------------------
-- Records of jexus_categories
-- ----------------------------
INSERT INTO "public"."jexus_categories" VALUES ('1', '0', '主版块', ' ', ' ', null, ' ', '1', '0', null, null);
INSERT INTO "public"."jexus_categories" VALUES ('2', '1', 'Jexus安装部署', ' ', ' ', null, ' ', '1', '1', null, null);
INSERT INTO "public"."jexus_categories" VALUES ('3', '1', 'Mono@Ubuntu', ' ', ' ', null, ' ', '1', '1', null, null);
INSERT INTO "public"."jexus_categories" VALUES ('4', '1', 'Mono@CentOS', ' ', ' ', null, ' ', '1', '1', null, null);
INSERT INTO "public"."jexus_categories" VALUES ('5', '1', '树莓派', ' ', ' ', null, ' ', '1', '1', null, null);

-- ----------------------------
-- Table structure for jexus_comments
-- ----------------------------
DROP TABLE IF EXISTS "public"."jexus_comments";
CREATE TABLE "public"."jexus_comments" (
"id" int4 DEFAULT nextval('jexus_comments_seq'::regclass) NOT NULL,
"fid" int4 NOT NULL,
"uid" int4 NOT NULL,
"content" text COLLATE "default",
"replytime" timestamptz(6)
)
WITH (OIDS=FALSE)

;

-- ----------------------------
-- Records of jexus_comments
-- ----------------------------
INSERT INTO "public"."jexus_comments" VALUES ('7', '10', '5', '213213', '2014-03-07 23:32:08.922093+08');
INSERT INTO "public"."jexus_comments" VALUES ('8', '10', '5', '3321', '2014-03-07 23:32:29.066297+08');
INSERT INTO "public"."jexus_comments" VALUES ('9', '10', '5', '213213123', '2014-03-07 23:34:09.322558+08');
INSERT INTO "public"."jexus_comments" VALUES ('10', '10', '5', '老大出品必属精品  支持宇内老大', '2014-04-01 17:53:56.558781+08');
INSERT INTO "public"."jexus_comments" VALUES ('11', '10', '5', '123213213', '2014-04-01 17:59:42.310599+08');
INSERT INTO "public"."jexus_comments" VALUES ('12', '10', '5', '123123', '2014-04-01 18:05:52.58521+08');

-- ----------------------------
-- Table structure for jexus_favorites
-- ----------------------------
DROP TABLE IF EXISTS "public"."jexus_favorites";
CREATE TABLE "public"."jexus_favorites" (
"id" int4 NOT NULL,
"uid" int4 NOT NULL,
"favorites" int4 NOT NULL,
"content" text COLLATE "default" NOT NULL
)
WITH (OIDS=FALSE)

;

-- ----------------------------
-- Records of jexus_favorites
-- ----------------------------

-- ----------------------------
-- Table structure for jexus_forums
-- ----------------------------
DROP TABLE IF EXISTS "public"."jexus_forums";
CREATE TABLE "public"."jexus_forums" (
"fid" int4 DEFAULT nextval('jexus_forums_fid_seq'::regclass) NOT NULL,
"cid" int2 NOT NULL,
"uid" int4 NOT NULL,
"ruid" int4,
"title" varchar(128) COLLATE "default",
"keywords" varchar(255) COLLATE "default",
"content" text COLLATE "default",
"addtime" timestamptz(6),
"updatetime" timestamptz(6),
"lastreply" timestamptz(6),
"views" int4,
"comments" int2,
"favorites" int8,
"closecomment" int2,
"is_top" int2 NOT NULL,
"is_hidden" int2 NOT NULL,
"ord" int8 NOT NULL
)
WITH (OIDS=FALSE)

;

-- ----------------------------
-- Records of jexus_forums
-- ----------------------------
INSERT INTO "public"."jexus_forums" VALUES ('10', '2', '5', '5', 'Jexus简介', null, 'Jexus&nbsp;web&nbsp;server&nbsp;for&nbsp;linux&nbsp;是一款基于.NET兼容环境，运行于Linux/unix操作系统之上，以支持ASP.NET为核心功能的高性能WEB服务器。<br />
Jexus&nbsp;V5.1有如下功能特点：<br />
01、支持ASP.NET。这是Jexus的核心功能。无论是稳定性、易用性还是并发承载能力、并行处理速度，Jexus对ASP.NET的支持都是非常优秀的；<br />
02、支持Fast-CGI。通Fast-CGI，Jexus能支持包括PHP在内的所有拥有Fast-CGI服务功能的WEB应用；<br />
03、具备基于正则表达式的强大的URL重写功能；<br />
04、具有强劲的反向代理功能。支持多目标负载均衡，支持本地网站与远程网站无缝整合；<br />
05、拥有强大的流媒体支持能力，支持FLV/F4V视频文件拖动播放，支持微软平滑流媒体技术；<br />
06、支持“服务器推送”技术，配备了相应的服务器端、客户端开发接口，是开发现代WEB应用的利器；<br />
07、具备可控的“ASP.NET前置缓存”，能最大限度地提高ASP.NET网站的承载能力和响应速度；<br />
08、支持Https，具有SSL加密数据安全传输能力；<br />
09、具有基础而实用的入侵检测功能，能自动终止已被识别的非法请求；<br />
10、安装部署非常简便，操作使用极为简单。', '2014-03-07 23:04:53.481013+08', '2014-03-07 23:04:53.481017+08', '2014-04-01 18:05:52.64115+08', '121', '8', null, null, '0', '0', '0');

-- ----------------------------
-- Table structure for jexus_links
-- ----------------------------
DROP TABLE IF EXISTS "public"."jexus_links";
CREATE TABLE "public"."jexus_links" (
"id" int2 NOT NULL,
"name" varchar(100) COLLATE "default",
"url" varchar(200) COLLATE "default",
"logo" varchar(200) COLLATE "default",
"is_hidden" int2 NOT NULL
)
WITH (OIDS=FALSE)

;

-- ----------------------------
-- Records of jexus_links
-- ----------------------------

-- ----------------------------
-- Table structure for jexus_notifications
-- ----------------------------
DROP TABLE IF EXISTS "public"."jexus_notifications";
CREATE TABLE "public"."jexus_notifications" (
"nid" int4 NOT NULL,
"fid" int4,
"suid" int4,
"nuid" int4 NOT NULL,
"ntype" int2,
"ntime" int4
)
WITH (OIDS=FALSE)

;

-- ----------------------------
-- Records of jexus_notifications
-- ----------------------------

-- ----------------------------
-- Table structure for jexus_page
-- ----------------------------
DROP TABLE IF EXISTS "public"."jexus_page";
CREATE TABLE "public"."jexus_page" (
"pid" int2 NOT NULL,
"title" varchar(100) COLLATE "default",
"content" text COLLATE "default",
"go_url" varchar(100) COLLATE "default",
"add_time" int4,
"is_hidden" int2
)
WITH (OIDS=FALSE)

;

-- ----------------------------
-- Records of jexus_page
-- ----------------------------

-- ----------------------------
-- Table structure for jexus_settings
-- ----------------------------
DROP TABLE IF EXISTS "public"."jexus_settings";
CREATE TABLE "public"."jexus_settings" (
"id" int2 NOT NULL,
"title" varchar(255) COLLATE "default" NOT NULL,
"value" text COLLATE "default" NOT NULL,
"type" int2 NOT NULL
)
WITH (OIDS=FALSE)

;

-- ----------------------------
-- Records of jexus_settings
-- ----------------------------
INSERT INTO "public"."jexus_settings" VALUES ('1', 'site_name', 'Jexus', '0');
INSERT INTO "public"."jexus_settings" VALUES ('2', 'welcome_tip', '欢迎访问Jexus社区', '0');
INSERT INTO "public"."jexus_settings" VALUES ('3', 'short_intro', 'Mono中文社区', '0');
INSERT INTO "public"."jexus_settings" VALUES ('4', 'show_captcha', 'on', '0');
INSERT INTO "public"."jexus_settings" VALUES ('5', 'site_run', '0', '0');
INSERT INTO "public"."jexus_settings" VALUES ('6', 'site_stats', '统计代码', '0');
INSERT INTO "public"."jexus_settings" VALUES ('7', 'site_keywords', '轻量 ?  易用  ?  社区系统', '0');
INSERT INTO "public"."jexus_settings" VALUES ('8', 'site_description', '最火的Mono社区', '0');
INSERT INTO "public"."jexus_settings" VALUES ('9', 'money_title', '银币', '0');
INSERT INTO "public"."jexus_settings" VALUES ('10', 'per_page_num', '20', '0');
INSERT INTO "public"."jexus_settings" VALUES ('11', 'is_rewrite', 'off', '0');
INSERT INTO "public"."jexus_settings" VALUES ('12', 'show_editor', 'off', '0');
INSERT INTO "public"."jexus_settings" VALUES ('13', 'comment_order', 'desc', '0');
INSERT INTO "public"."jexus_settings" VALUES ('14', 'storage_set', 'local', '0');
INSERT INTO "public"."jexus_settings" VALUES ('15', 'auto_tag', 'off', '0');
INSERT INTO "public"."jexus_settings" VALUES ('16', 'sys_url', ' ', '0');

-- ----------------------------
-- Table structure for jexus_tags
-- ----------------------------
DROP TABLE IF EXISTS "public"."jexus_tags";
CREATE TABLE "public"."jexus_tags" (
"tag_id" int4 NOT NULL,
"tag_title" varchar(30) COLLATE "default" NOT NULL,
"forums" int4 NOT NULL
)
WITH (OIDS=FALSE)

;

-- ----------------------------
-- Records of jexus_tags
-- ----------------------------

-- ----------------------------
-- Table structure for jexus_tags_relation
-- ----------------------------
DROP TABLE IF EXISTS "public"."jexus_tags_relation";
CREATE TABLE "public"."jexus_tags_relation" (
"tag_id" int4 NOT NULL,
"fid" int4
)
WITH (OIDS=FALSE)

;

-- ----------------------------
-- Records of jexus_tags_relation
-- ----------------------------

-- ----------------------------
-- Table structure for jexus_user_follow
-- ----------------------------
DROP TABLE IF EXISTS "public"."jexus_user_follow";
CREATE TABLE "public"."jexus_user_follow" (
"follow_id" int8 NOT NULL,
"uid" int8 NOT NULL,
"follow_uid" int8 NOT NULL,
"addtime" int4 NOT NULL
)
WITH (OIDS=FALSE)

;

-- ----------------------------
-- Records of jexus_user_follow
-- ----------------------------

-- ----------------------------
-- Table structure for jexus_user_groups
-- ----------------------------
DROP TABLE IF EXISTS "public"."jexus_user_groups";
CREATE TABLE "public"."jexus_user_groups" (
"gid" int4 NOT NULL,
"group_type" int2 NOT NULL,
"group_name" varchar(50) COLLATE "default",
"usernum" int4 NOT NULL
)
WITH (OIDS=FALSE)

;

-- ----------------------------
-- Records of jexus_user_groups
-- ----------------------------
INSERT INTO "public"."jexus_user_groups" VALUES ('1', '0', '管理员', '1');
INSERT INTO "public"."jexus_user_groups" VALUES ('2', '1', '版主', '0');
INSERT INTO "public"."jexus_user_groups" VALUES ('3', '2', '普通会员', '0');

-- ----------------------------
-- Table structure for jexus_users
-- ----------------------------
DROP TABLE IF EXISTS "public"."jexus_users";
CREATE TABLE "public"."jexus_users" (
"uid" int4 DEFAULT nextval('jexus_users_uid_seq'::regclass) NOT NULL,
"username" varchar(20) COLLATE "default",
"password" char(32) COLLATE "default",
"openid" char(32) COLLATE "default" NOT NULL,
"email" varchar(50) COLLATE "default",
"avatar" varchar(100) COLLATE "default",
"homepage" varchar(50) COLLATE "default",
"money" int4,
"signature" text COLLATE "default",
"forums" int4,
"replies" int4,
"notices" int2,
"follows" int4 NOT NULL,
"regtime" timestamptz(6),
"lastlogin" timestamptz(6),
"lastpost" timestamptz(6),
"qq" varchar(20) COLLATE "default",
"group_type" int2 NOT NULL,
"gid" int2 NOT NULL,
"ip" char(15) COLLATE "default",
"location" varchar(128) COLLATE "default",
"token" varchar(40) COLLATE "default",
"introduction" text COLLATE "default",
"is_active" int2 NOT NULL
)
WITH (OIDS=FALSE)

;

-- ----------------------------
-- Records of jexus_users
-- ----------------------------
INSERT INTO "public"."jexus_users" VALUES ('4', 'admin', '34F85CA80EC353D3052B8A2D3973A0C5', '                                ', 'djs@olexe.cn', null, null, '100', ' ', '0', '0', '0', '0', '2014-03-07 20:55:42.548339+08', null, null, ' ', '0', '1', '222.209.110.12 ', ' ', null, ' ', '1');
INSERT INTO "public"."jexus_users" VALUES ('5', '小白', '34F85CA80EC353D3052B8A2D3973A0C5', '                                ', 'djs@olexe.cn', '/uploads/avatar/5', null, null, null, null, null, null, '0', '2014-03-07 23:00:22.889554+08', null, null, null, '2', '3', '222.209.110.12 ', null, null, null, '1');
INSERT INTO "public"."jexus_users" VALUES ('6', 'admin', '34F85CA80EC353D3052B8A2D3973A0C5', '                                ', 'djs@olexe.cn', '/uploads/avatar', null, null, null, null, null, null, '0', '2014-04-02 09:08:07.123937+08', null, null, null, '2', '3', '118.122.94.212 ', null, null, null, '1');
INSERT INTO "public"."jexus_users" VALUES ('7', 'test', '34F85CA80EC353D3052B8A2D3973A0C5', '                                ', 'abc@olexe.cn', '/uploads/avatar', null, null, null, null, null, null, '0', '2014-04-02 09:17:58.007583+08', null, null, null, '2', '3', '::1            ', null, null, null, '1');
INSERT INTO "public"."jexus_users" VALUES ('8', '黄鹤', '25D55AD283AA400AF464C76D713C07AD', '                                ', 'hd@qq.com', '/uploads/avatar', null, null, null, null, null, null, '0', '2014-04-03 22:44:17.088248+08', null, null, null, '2', '3', '222.210.219.180', null, null, null, '1');
INSERT INTO "public"."jexus_users" VALUES ('9', 'test2', '25D55AD283AA400AF464C76D713C07AD', '                                ', '13@qq.com', '/uploads/avatar', null, null, null, null, null, null, '0', '2014-04-03 22:45:07.238723+08', null, null, null, '2', '3', '222.210.219.180', null, null, null, '1');
INSERT INTO "public"."jexus_users" VALUES ('10', 'test3', '25D55AD283AA400AF464C76D713C07AD', '                                ', 'da@qc.com', '/uploads/avatar', null, null, null, null, null, null, '0', '2014-04-03 22:45:48.286377+08', null, null, null, '2', '3', '222.210.219.180', null, null, null, '1');
INSERT INTO "public"."jexus_users" VALUES ('11', 'kevin', '92D7DDD2A010C59511DC2905B7E14F64', '                                ', 'xklqx@qq.com', '/uploads/avatar', null, null, null, null, null, null, '0', '2014-04-22 10:12:52.221635+08', null, null, null, '2', '3', '114.250.80.191 ', null, null, null, '1');
INSERT INTO "public"."jexus_users" VALUES ('12', 'WanAn', '9DD7B150657EBED4D042448B897512DD', '                                ', '841228093@qq.com', '/uploads/avatar', null, null, null, null, null, null, '0', '2014-04-22 10:12:52.94406+08', null, null, null, '2', '3', '113.109.186.68 ', null, null, null, '1');

-- ----------------------------
-- Alter Sequences Owned By 
-- ----------------------------
ALTER SEQUENCE "public"."jexus_forums_fid_seq" OWNED BY "jexus_forums"."fid";
ALTER SEQUENCE "public"."jexus_users_uid_seq" OWNED BY "jexus_users"."uid";

-- ----------------------------
-- Primary Key structure for table jexus_categories
-- ----------------------------
ALTER TABLE "public"."jexus_categories" ADD PRIMARY KEY ("cid", "pid");

-- ----------------------------
-- Primary Key structure for table jexus_comments
-- ----------------------------
ALTER TABLE "public"."jexus_comments" ADD PRIMARY KEY ("id", "fid", "uid");

-- ----------------------------
-- Primary Key structure for table jexus_favorites
-- ----------------------------
ALTER TABLE "public"."jexus_favorites" ADD PRIMARY KEY ("id", "uid");

-- ----------------------------
-- Primary Key structure for table jexus_forums
-- ----------------------------
ALTER TABLE "public"."jexus_forums" ADD PRIMARY KEY ("fid", "cid", "uid");

-- ----------------------------
-- Primary Key structure for table jexus_links
-- ----------------------------
ALTER TABLE "public"."jexus_links" ADD PRIMARY KEY ("id");

-- ----------------------------
-- Primary Key structure for table jexus_notifications
-- ----------------------------
ALTER TABLE "public"."jexus_notifications" ADD PRIMARY KEY ("nid", "nuid");

-- ----------------------------
-- Primary Key structure for table jexus_page
-- ----------------------------
ALTER TABLE "public"."jexus_page" ADD PRIMARY KEY ("pid");

-- ----------------------------
-- Primary Key structure for table jexus_settings
-- ----------------------------
ALTER TABLE "public"."jexus_settings" ADD PRIMARY KEY ("id", "title", "type");

-- ----------------------------
-- Primary Key structure for table jexus_tags
-- ----------------------------
ALTER TABLE "public"."jexus_tags" ADD PRIMARY KEY ("tag_id");

-- ----------------------------
-- Primary Key structure for table jexus_user_follow
-- ----------------------------
ALTER TABLE "public"."jexus_user_follow" ADD PRIMARY KEY ("follow_id", "uid", "follow_uid");

-- ----------------------------
-- Primary Key structure for table jexus_user_groups
-- ----------------------------
ALTER TABLE "public"."jexus_user_groups" ADD PRIMARY KEY ("gid", "group_type");

-- ----------------------------
-- Primary Key structure for table jexus_users
-- ----------------------------
ALTER TABLE "public"."jexus_users" ADD PRIMARY KEY ("uid", "group_type");
