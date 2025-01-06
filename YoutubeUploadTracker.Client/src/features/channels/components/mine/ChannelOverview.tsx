import { CloseButton, TextInput } from "@mantine/core";
import { Channel } from "../../types";
import ChannelGalleryItem from "./ChannelGalleryItem";
import SuggestChannelImport from "./SuggestChannelImport";
import { useRef, useState } from "react";

export type Props = {
  injectedChannels: Channel[];
};

export default function ChannelOverview({ injectedChannels }: Props) {
  const [nameQuery, setNameQuery] = useState("");
  const [channels, setChannels] = useState(injectedChannels);

  const updateTimeoutRef = useRef<ReturnType<typeof setTimeout> | undefined>(
    undefined
  );

  const updateNameQuery = (value?: string) => {
    clearTimeout(updateTimeoutRef.current);
    updateTimeoutRef.current = setTimeout(() => {}, 500);

    setNameQuery(value ?? "");
  };

  if (channels.length === 0) {
    return <SuggestChannelImport />;
  }

  return (
    <div className="flex flex-col gap-4">
      <div className="flex flex-row gap-4">
        <TextInput
          placeholder="Filter by name"
          label="Name"
          value={nameQuery}
          onChange={(x) => updateNameQuery(x.currentTarget.value)}
          rightSection={
            <CloseButton
              aria-label="Clear input"
              onClick={() => updateNameQuery(undefined)}
              style={{ display: nameQuery ? undefined : "none" }}
            />
          }
        />
      </div>
      <div className="grid grid-cols-4 gap-5">
        {channels.map((x) => (
          <ChannelGalleryItem channel={x} />
        ))}
      </div>
    </div>
  );
}
