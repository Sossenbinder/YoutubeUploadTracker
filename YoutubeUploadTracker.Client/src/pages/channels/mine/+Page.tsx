import { Channel } from "@root/src/features/channels/types";
import { PageData } from "../../types";
import { useData } from "vike-react/useData";
import ChannelOverview from "@root/src/features/channels/components/mine/ChannelOverview";
import { httpGet } from "@root/src/features/common/utils/ssrRequests";
import { PageContext } from "vike/types";

type ChannelsQueryResponse = {
  channels: Channel[];
  pageToken?: string;
};

export async function data(
  pageContext: PageContext
): Promise<ChannelsQueryResponse> {
  const channels = await httpGet<ChannelsQueryResponse>(
    pageContext,
    "/channels?mine=true"
  );

  console.log(channels);

  return channels ?? { channels: [] };
}

export default function () {
  const data = useData<PageData<ChannelsQueryResponse>>();

  return <ChannelOverview injectedChannels={data.channels} />;
}
