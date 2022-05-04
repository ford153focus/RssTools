// ReSharper disable InconsistentNaming
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable CollectionNeverUpdated.Global
#pragma warning disable CS8618

namespace RssSharedLibrary.Models;

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class More
{
    public string link { get; set; }
    public bool main { get; set; }
    public bool delete { get; set; }
    public string text { get; set; }
    public string stat { get; set; }
}

public class Auth
{
    public bool is_authorized { get; set; }
}

public class Favorites
{
    public string id { get; set; }
    public string text { get; set; }
    public string url { get; set; }
    public string icon { get; set; }
    public List<object> values { get; set; }
}

public class Feedback
{
    public string id { get; set; }
    public string text { get; set; }
    public string url { get; set; }
    public List<object> values { get; set; }
    public CancelLess cancel_less { get; set; }
    public CancelBlock cancel_block { get; set; }
    public CancelMore cancel_more { get; set; }
    public More more { get; set; }
    public Block block { get; set; }
    public Less less { get; set; }
    public Complain complain { get; set; }
}

public class Country
{
    public string id { get; set; }
    public string text { get; set; }
    public string url { get; set; }
    public string icon { get; set; }
    public List<object> values { get; set; }
}

public class License
{
    public string id { get; set; }
    public string text { get; set; }
    public string url { get; set; }
    public List<object> values { get; set; }
}

public class Blocked
{
    public string id { get; set; }
    public string text { get; set; }
    public string url { get; set; }
    public string icon { get; set; }
    public List<object> values { get; set; }
}

public class Menu
{
    public Favorites favorites { get; set; }
    public Feedback feedback { get; set; }
    public Country country { get; set; }
    public License license { get; set; }
    public Blocked blocked { get; set; }
    public string type { get; set; }
    public Enable enable { get; set; }
    public Disable disable { get; set; }
}

public class Bulk
{
    public string link { get; set; }
}

public class Result
{
    public List<object> items { get; set; }
    public Bulk bulk { get; set; }
}

public class CurrentUserSubscriptions
{
    public Result result { get; set; }
    public string status { get; set; }
}

public class CurrentUserInterests
{
    public Result result { get; set; }
    public string status { get; set; }
}

public class Links
{
    public string all_channels_link { get; set; }
    public string passport { get; set; }
    public string support_link { get; set; }
    public string subscriptions_list_link { get; set; }
    public string logout { get; set; }
    public string logout_tmpl { get; set; }
}

public class StatEvents
{
    public string feedback_cancel_block { get; set; }
    public string phone_call_cancel { get; set; }
    public string complain_click { get; set; }
    public string complain_show { get; set; }
    public string feedback_favourite { get; set; }
    public string phone_call_click { get; set; }
    public string user_menu_favourite_click { get; set; }
    public string feedback_cancel_favourite { get; set; }
    public string phone_call_show { get; set; }
    public string login_popup_enter { get; set; }
    public string login_popup_later { get; set; }
    public string share_click { get; set; }
    public string login_suggest_show { get; set; }
    public string login_popup_show { get; set; }
    public string arrow_down_click { get; set; }
    public string user_menu_cart_click { get; set; }
    public string feedback_block { get; set; }
    public string phone_call_call { get; set; }
    public string login_suggest_close { get; set; }
    public string messenger_click { get; set; }
    public string arrow_down_show { get; set; }
    public string auth_click { get; set; }
    public string auth_login { get; set; }
}

public class Stats
{
    public string click_teaser { get; set; }
    public string show { get; set; }
    public string click { get; set; }
    public string login_popup_enter { get; set; }
    public string login_popup_later { get; set; }
    public string login_suggest_show { get; set; }
    public string login_popup_show { get; set; }
    public string swipe { get; set; }
    public string @short { get; set; }
    public string login_suggest_close { get; set; }
    public string show_teaser { get; set; }
    public string auth_click { get; set; }
    public string auth_login { get; set; }
    public string play { get; set; }
    public string sound_off { get; set; }
    public string heartbeat { get; set; }
    public string autopause { get; set; }
    public string pause { get; set; }
    public string autoplay { get; set; }
    public string sound_on { get; set; }
    public string end { get; set; }
}

public class CardColors
{
    public string card { get; set; }
    public string text { get; set; }
    public string button { get; set; }
    public string button_text { get; set; }
}

public class PromoLabel
{
    public string text { get; set; }
    public string text_color { get; set; }
    public string channel_info_subscribed { get; set; }
    public string channel_info_unsubscribed { get; set; }
}

public class LogoSizes
{
    public string framed_100x128_1x { get; set; }
    public string framed_212x280_1x { get; set; }
    public string framed_202x260_1x { get; set; }
    public string framed_132x176_1x { get; set; }
}

public class Source
{
    public string logo { get; set; }
    public string framed_logo { get; set; }
    public LogoSizes logo_sizes { get; set; }
    public string logo_background_color { get; set; }
    public string title_background_color { get; set; }
    public string title_color { get; set; }
    public string header_background_color { get; set; }
    public string feed_link { get; set; }
    public string feed_share_link { get; set; }
    public string feed_api_link { get; set; }
    public string id { get; set; }
    public string strongest_id { get; set; }
    public string type { get; set; }
    public string status { get; set; }
    public string title { get; set; }
    public int vacuum_counter_id { get; set; }
    public int subscribers { get; set; }
    public bool is_verified { get; set; }
    public string url { get; set; }
    public string subtitle { get; set; }
    public string fresh_channel_title { get; set; }
    public int? metrica_id { get; set; }
    public string multifeed_api_link { get; set; }
    public List<object> social_links { get; set; }
    public string subscribers_short { get; set; }
    public List<object> chat_references { get; set; }
    public bool is_challenge { get; set; }
    public string subscribers_formatted { get; set; }
    public bool is_tag { get; set; }
    public string recommendations_channels_link { get; set; }
}

public class CancelLess
{
    public string text { get; set; }
    public string stat { get; set; }
    public string button_text { get; set; }
}

public class CancelBlock
{
    public string text { get; set; }
    public string stat { get; set; }
    public string button_text { get; set; }
    public string text_color { get; set; }
    public string background_color { get; set; }
}

public class CancelMore
{
    public string stat { get; set; }
}

public class Block
{
    public bool main { get; set; }
    public bool delete { get; set; }
    public string text { get; set; }
    public string stat { get; set; }
}

public class Less
{
    public bool main { get; set; }
    public bool delete { get; set; }
    public string text { get; set; }
    public string stat { get; set; }
}

public class Complain
{
    public string text { get; set; }
    public string link { get; set; }
}

public class Enable
{
    public string title { get; set; }
    public string subtitle { get; set; }
    public string icon { get; set; }
    public string link { get; set; }
}

public class Disable
{
    public string title { get; set; }
    public string subtitle { get; set; }
    public string icon { get; set; }
}

public class SharingMenu
{
    public string title { get; set; }
    public string link_item_name { get; set; }
    public string link_success_message { get; set; }
    public string more_item_name { get; set; }
    public string assets { get; set; }
    public string asset_error_msg { get; set; }
    public string instagram_item_name { get; set; }
    public string facebook_item_name { get; set; }
    public string repost_url { get; set; }
}

public class VideoInfo
{
    public string video_content_id { get; set; }
}

public class AdditionalParams
{
    public string from { get; set; }
    public string ppi { get; set; }
    public string from_block { get; set; }
    public string puid { get; set; }
    public string video_content_id { get; set; }
    public string strongest_id { get; set; }
    public string item_id { get; set; }
    public string rid { get; set; }
    public string stream_block { get; set; }
}

public class Video
{
    public string provider { get; set; }
    public string id { get; set; }
    public List<string> streams { get; set; }
    public string player { get; set; }
    public bool loop { get; set; }
    public bool has_sound { get; set; }
    public bool autoplay { get; set; }
    public int replay_count { get; set; }
    public int width { get; set; }
    public int height { get; set; }
    public int duration { get; set; }
    public VideoInfo video_info { get; set; }
    public int views { get; set; }
    public string views_formatted { get; set; }
    public string userAgent { get; set; }
    public string heartbeat { get; set; }
    public string end { get; set; }
    public AdditionalParams additional_params { get; set; }
    public string video_feed_url { get; set; }
    public string same_publisher_video_url { get; set; }
    public bool isShortVideo { get; set; }
    public bool is_short_video { get; set; }
    public List<int> heartbeat_pos { get; set; }
}

public class VideoPreview
{
}

public class VideoPixels
{
}

public class Item
{
    public StatEvents stat_events { get; set; }
    public Stats stats { get; set; }
    public string image { get; set; }
    public string image_squared { get; set; }
    public CardColors card_colors { get; set; }
    public int channel_owner_uid { get; set; }
    public string publisher_id { get; set; }
    public string comments_document_id { get; set; }
    public string publication_id { get; set; }
    public string comments_link { get; set; }
    public string all_comments_link { get; set; }
    public string one_comment_link { get; set; }
    public string comments_token { get; set; }
    public PromoLabel promo_label { get; set; }
    public string creation_time { get; set; }
    public string title { get; set; }
    public bool read { get; set; }
    public string logo { get; set; }
    public string domain_title { get; set; }
    public string domain { get; set; }
    public string date { get; set; }
    public Source source { get; set; }
    public bool precache { get; set; }
    public string link { get; set; }
    public List<object> pixels { get; set; }
    public bool notifications { get; set; }
    public Feedback feedback { get; set; }
    public List<Menu> menu { get; set; }
    public bool is_low_resolution_device { get; set; }
    public int pos { get; set; }
    public string publication_object_id { get; set; }
    public string id { get; set; }
    public string type { get; set; }
    public bool is_favorited { get; set; }
    public bool is_promoted { get; set; }
    public bool is_promo_publication { get; set; }
    public string card_type { get; set; }
    public string item_type { get; set; }
    public string share_link { get; set; }
    public More more { get; set; }
    public string text { get; set; }
    public bool require_user_data { get; set; }
    public string publication_date { get; set; }
    public bool is_pinned { get; set; }
    public List<object> interests { get; set; }
    public SharingMenu sharing_menu { get; set; }
    public Video video { get; set; }
    public bool? has_tags { get; set; }
    public bool? has_description { get; set; }
    public bool? show_div_controls { get; set; }
    public VideoPreview video_preview { get; set; }
    public VideoPixels video_pixels { get; set; }
}

public class UserAdditionalInfo
{
    public int subscriptionsCount { get; set; }
}

public class Exp
{
    public string video_statistic { get; set; }
    public string zenkitx__debug { get; set; }
    public string comments_badge { get; set; }
    public string enable_editor_without_bundle { get; set; }
}

public class ClientDefinition
{
}

public class ExperimentsData
{
}

public class SocialInfo
{
}

public class PopupContent
{
    public string title { get; set; }
    public string text { get; set; }
}

public class CancelFavourite
{
    public string text { get; set; }
    public string text_color { get; set; }
    public string background_color { get; set; }
}

public class Share
{
    public string text { get; set; }
    public string click_url { get; set; }
}

public class Favourite
{
    public string text { get; set; }
    public string text_color { get; set; }
    public string background_color { get; set; }
}

public class Actions
{
    public CancelBlock cancel_block { get; set; }
    public CancelFavourite cancel_favourite { get; set; }
    public Block block { get; set; }
    public Share share { get; set; }
    public Favourite favourite { get; set; }
}

public class Header
{
    public StatEvents stat_events { get; set; }
    public string bulk_params { get; set; }
    public Source source { get; set; }
    public string title { get; set; }
    public string subtitle { get; set; }
    public string image { get; set; }
    public string status { get; set; }
    public List<PopupContent> popup_content { get; set; }
    public string subscribers_title { get; set; }
    public string audience_title { get; set; }
    public Actions actions { get; set; }
    public List<object> social_links { get; set; }
    public List<Menu> menu { get; set; }
    public string tag_status { get; set; }
}

public class Status
{
    public string type { get; set; }
}

public class Light
{
    public string specific_logo_star { get; set; }
    public string specific_logo_star_inverted { get; set; }
    public string specific_logo_circle { get; set; }
    public string icefeed_interest_text { get; set; }
    public string icefeed_interest_checkbox { get; set; }
    public string icefeed_interest_checkbox_selected { get; set; }
    public string icefeed_interest_selected { get; set; }
    public string inverted_text_color { get; set; }
    public string tooltip_background { get; set; }
    public string fill_6 { get; set; }
    public string fill_12 { get; set; }
    public string fill_18 { get; set; }
    public string fill_24 { get; set; }
    public string fill_30 { get; set; }
    public string applied_image_fill { get; set; }
    public string applied_overlay { get; set; }
    public string applied_hover { get; set; }
    public string applied_stroke { get; set; }
    public string applied_separator { get; set; }
    public string applied_media_transparent_bar { get; set; }
    public string applied_interview_inactive { get; set; }
    public string applied_text_attention_background { get; set; }
    public string applied_trimmer_yellow { get; set; }
    public string applied_trimmer_red { get; set; }
    public string applied_trimmer_blue { get; set; }
    public string applied_trimmer_green { get; set; }
    public string applied_chip_button_active { get; set; }
    public string background_overflow { get; set; }
    public string background_primary { get; set; }
    public string background_secondary { get; set; }
    public string background_tertiary { get; set; }
    public string text_and_icons_primary { get; set; }
    public string text_and_icons_secondary { get; set; }
    public string text_and_icons_tertiary { get; set; }
    public string text_and_icons_disabled { get; set; }
    public string text_and_icons_media_bg { get; set; }
    public string text_and_icons_colored_bg { get; set; }
    public string text_and_icons_primary_low { get; set; }
    public string text_and_icons_tertiary_low { get; set; }
    public string accents_red { get; set; }
    public string accents_yellow { get; set; }
    public string accents_blue { get; set; }
    public string accents_green { get; set; }
    public string accents_violet { get; set; }
    public string accents_orange { get; set; }
    public string accents_pink { get; set; }
    public string accents_light_green { get; set; }
    public string accents_blue_violet { get; set; }
    public string technical_transparent { get; set; }
    public string brand_alice { get; set; }
    public string brand_yandex { get; set; }
}

public class Dark
{
    public string specific_logo_star { get; set; }
    public string specific_logo_star_inverted { get; set; }
    public string specific_logo_circle { get; set; }
    public string icefeed_interest_text { get; set; }
    public string icefeed_interest_checkbox { get; set; }
    public string icefeed_interest_checkbox_selected { get; set; }
    public string icefeed_interest_selected { get; set; }
    public string inverted_text_color { get; set; }
    public string tooltip_background { get; set; }
    public string fill_6 { get; set; }
    public string fill_12 { get; set; }
    public string fill_18 { get; set; }
    public string fill_24 { get; set; }
    public string fill_30 { get; set; }
    public string applied_image_fill { get; set; }
    public string applied_overlay { get; set; }
    public string applied_hover { get; set; }
    public string applied_stroke { get; set; }
    public string applied_separator { get; set; }
    public string applied_media_transparent_bar { get; set; }
    public string applied_interview_inactive { get; set; }
    public string applied_text_attention_background { get; set; }
    public string applied_trimmer_yellow { get; set; }
    public string applied_trimmer_red { get; set; }
    public string applied_trimmer_blue { get; set; }
    public string applied_trimmer_green { get; set; }
    public string applied_chip_button_active { get; set; }
    public string background_overflow { get; set; }
    public string background_primary { get; set; }
    public string background_secondary { get; set; }
    public string background_tertiary { get; set; }
    public string text_and_icons_primary { get; set; }
    public string text_and_icons_secondary { get; set; }
    public string text_and_icons_tertiary { get; set; }
    public string text_and_icons_disabled { get; set; }
    public string text_and_icons_media_bg { get; set; }
    public string text_and_icons_colored_bg { get; set; }
    public string text_and_icons_primary_low { get; set; }
    public string text_and_icons_tertiary_low { get; set; }
    public string accents_red { get; set; }
    public string accents_yellow { get; set; }
    public string accents_blue { get; set; }
    public string accents_green { get; set; }
    public string accents_violet { get; set; }
    public string accents_orange { get; set; }
    public string accents_pink { get; set; }
    public string accents_light_green { get; set; }
    public string accents_blue_violet { get; set; }
    public string technical_transparent { get; set; }
    public string brand_alice { get; set; }
    public string brand_yandex { get; set; }
}

public class ColorPalettes
{
    public Light light { get; set; }
    public Dark dark { get; set; }
}

public class YandexZenResponse
{
    public More more { get; set; }
    public string rid { get; set; }
    public int ttl { get; set; }
    public int store_ttl { get; set; }
    public int generate_time { get; set; }
    public bool have_zen { get; set; }
    public bool show_zen { get; set; }
    public bool ice_start { get; set; }
    public string group_ids { get; set; }
    public Auth auth { get; set; }
    public string user_status_on_publishers_platform { get; set; }
    public Menu menu { get; set; }
    public CurrentUserSubscriptions current_user_subscriptions { get; set; }
    public CurrentUserInterests current_user_interests { get; set; }
    public Links links { get; set; }
    public List<Item> items { get; set; }
    public List<object> sidebar_items { get; set; }
    public List<object> left_sidebar_items { get; set; }
    public string user_avatar { get; set; }
    public UserAdditionalInfo user_additional_info { get; set; }
    public Exp exp { get; set; }
    public ClientDefinition client_definition { get; set; }
    public ExperimentsData experiments_data { get; set; }
    public SocialInfo _social_info { get; set; }
    public List<string> migration_flags { get; set; }
    public bool merged_channel_profile { get; set; }
    public Header header { get; set; }
    public Status status { get; set; }
    public List<object> pinned_publication_ids { get; set; }
    public List<object> ordered_ids { get; set; }
    public List<object> tabs { get; set; }
    public ColorPalettes color_palettes { get; set; }
}